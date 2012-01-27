<?php

$skip_files = array("./filelist.php", "./version.txt");

function scan_dir($path)
{   
    global $skip_files;
    
    $dir = opendir($path);
    while (false !== ($file = readdir($dir)))
    {
        if ($file == '.' or $file == '..') continue;
        if (is_dir($path . $file))
        {
            scan_dir($path . $file . '/');
            continue;
        }
        $fname = $path . $file;
        if (in_array($fname, $skip_files))
            continue;

        $md5 = md5_file($path.$file);
        $size = filesize($path.$file);
        $fname = substr($fname, 2, strlen($fname)-2);
        print "$md5|$size|$fname\n";
    }
    closedir($dir);
}

header("Content-Type: text/plain");
$version = trim(file_get_contents("./version.txt"));
print $version."\n";
scan_dir('./');

?>