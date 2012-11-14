#include <QtGui>
#include "mainwindow.h"

int main(int argc, char *argv[])
{
    Q_INIT_RESOURCE(lanexchange);

    // init application
    QApplication app(argc, argv);
    app.setOrganizationName("SkivSoft");
    app.setApplicationName("LanExchange");

    // show main window 
    MAINWINDOW w;
    w.show();
    // run
    return app.exec();
}
