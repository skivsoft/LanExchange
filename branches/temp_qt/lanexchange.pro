QT       += core gui

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

TARGET = lanexchange
TEMPLATE = app


SOURCES += main.cpp\
        mainwindow.cpp \
        foldernavigationwidget.cpp

HEADERS  += mainwindow.h \
            foldernavigationwidget.h

FORMS    += mainwindow.ui

RESOURCES += lanexchange.qrc

RC_FILE = lanexchange.rc
