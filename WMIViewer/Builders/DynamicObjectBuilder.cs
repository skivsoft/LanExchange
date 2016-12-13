using System;
using System.Globalization;
using System.Management;
using WMIViewer.Model;

namespace WMIViewer.Builders
{
    public class DynamicObjectBuilder
    {
        private ManagementClass wmiClass;
        private ManagementObject wmiObject;

        public DynamicObjectBuilder SetClass(ManagementClass managementClass)
        {
            wmiClass = managementClass;
            return this;
        }

        public DynamicObjectBuilder SetObject(ManagementObject managementObject)
        {
            wmiObject = managementObject;
            return this;
        }

        public object Build()
        {
            var dynObj = new DynamicObject();
            foreach (var prop in wmiObject.Properties)
            {
                // skip array of bytes
                if (prop.Type == CimType.UInt8 && prop.IsArray)
                    continue;

                var classProp = wmiClass.Properties[prop.Name];
                var info = new QualifiersInfo(classProp.Qualifiers);
                if (info.IsCimKey) continue;
                var category = prop.Type.ToString();
                var description = info.Description;
                var isReadOnly = info.IsReadOnly;
                switch (prop.Type)
                {
                    // A signed 16-bit integer. This value maps to the System.Int16 type.
                    case CimType.SInt16:
                        DynObjAddProperty<short>(dynObj, prop, description, category, isReadOnly);
                        break;

                    // A signed 32-bit integer. This value maps to the System.Int32 type.
                    case CimType.SInt32:
                        DynObjAddProperty<int>(dynObj, prop, description, category, isReadOnly);
                        break;

                    // A floating-point 32-bit number. This value maps to the System.Single type.
                    case CimType.Real32:
                        DynObjAddProperty<float>(dynObj, prop, description, category, isReadOnly);
                        break;

                    // A floating point 64-bit number. This value maps to the System.Double type.
                    case CimType.Real64:
                        DynObjAddProperty<double>(dynObj, prop, description, category, isReadOnly);
                        break;

                    // A string. This value maps to the System.String type.
                    case CimType.String:
                        DynObjAddProperty<string>(dynObj, prop, description, category, isReadOnly);
                        break;

                    // A Boolean. This value maps to the System.Boolean type.
                    case CimType.Boolean:
                        DynObjAddProperty<bool>(dynObj, prop, description, category, isReadOnly);
                        break;

                    // An embedded object. Note that embedded objects differ from references in
                    // that the embedded object does not have a path and its lifetime is identical
                    // to the lifetime of the containing object. This value maps to the System.Object
                    // type.
                    case CimType.Object:
                        DynObjAddProperty<object>(dynObj, prop, description, category, isReadOnly);
                        break;

                    // A signed 8-bit integer. This value maps to the System.SByte type.
                    case CimType.SInt8:
                        DynObjAddProperty<sbyte>(dynObj, prop, description, category, isReadOnly);
                        break;

                    // An unsigned 8-bit integer. This value maps to the System.Byte type.
                    case CimType.UInt8:
                        DynObjAddProperty<byte>(dynObj, prop, description, category, isReadOnly);
                        break;

                    // An unsigned 16-bit integer. This value maps to the System.UInt16 type.
                    case CimType.UInt16:
                        DynObjAddProperty<ushort>(dynObj, prop, description, category, isReadOnly);
                        break;

                    // An unsigned 32-bit integer. This value maps to the System.UInt32 type.
                    case CimType.UInt32:
                        DynObjAddProperty<uint>(dynObj, prop, description, category, isReadOnly);
                        break;

                    // A signed 64-bit integer. This value maps to the System.Int64 type.
                    case CimType.SInt64:
                        DynObjAddProperty<long>(dynObj, prop, description, category, isReadOnly);
                        break;

                    // An unsigned 64-bit integer. This value maps to the System.UInt64 type.
                    case CimType.UInt64:
                        DynObjAddProperty<ulong>(dynObj, prop, description, category, isReadOnly);
                        break;

                    // A date or time value, represented in a string in DMTF date/time format: yyyymmddHHMMSS.mmmmmmsUUU,
                    // where yyyymmdd is the date in year/month/day; HHMMSS is the time in hours/minutes/seconds;
                    // mmmmmm is the number of microseconds in 6 digits; and sUUU is a sign (+ or
                    // -) and a 3-digit UTC offset. This value maps to the System.DateTime type.
                    case CimType.DateTime:
                        if (prop.Value == null)
                            dynObj.AddPropertyNull<DateTime>(prop.Name, prop.Name, description, category, isReadOnly, null);
                        else
                            dynObj.AddProperty(prop.Name, WmiHelper.ToDateTime(prop.Value.ToString()), description, category, isReadOnly);
                        break;

                    // A reference to another object. This is represented by a string containing
                    // the path to the referenced object. This value maps to the System.Int16 type.
                    case CimType.Reference:
                        DynObjAddProperty<short>(dynObj, prop, description, category, isReadOnly);
                        break;

                    // A 16-bit character. This value maps to the System.Char type.
                    case CimType.Char16:
                        DynObjAddProperty<char>(dynObj, prop, description, category, isReadOnly);
                        break;
                    default:
                        string value = prop.Value == null ? null : prop.Value.ToString();
                        dynObj.AddProperty(string.Format(CultureInfo.InvariantCulture, "{0} : {1}", prop.Name, prop.Type), value, description, "Unknown", isReadOnly);
                        break;
                }
            }

            return dynObj;
        }

        private static void DynObjAddProperty<T>(DynamicObject dynamicObj, PropertyData prop, string description, string category, bool isReadOnly)
        {
            if (prop.Value == null)
                dynamicObj.AddPropertyNull<T>(prop.Name, prop.Name, description, category, isReadOnly, null);
            else
                if (prop.IsArray)
                dynamicObj.AddProperty(prop.Name, (T[])prop.Value, description, category, isReadOnly);
            else
                dynamicObj.AddProperty(prop.Name, (T)prop.Value, description, category, isReadOnly);
        }
    }
}
