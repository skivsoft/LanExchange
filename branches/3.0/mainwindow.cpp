#include <QtGui>

#include "mainwindow.h"
#include "foldernavigationwidget.h"

MAINWINDOW::MAINWINDOW(QWidget *parent, Qt::WFlags flags) : QMainWindow(parent, flags)
{
    ui.setupUi(this);

    ProjectExplorer::Internal::FolderNavigationWidget *Widget = new ProjectExplorer::Internal::FolderNavigationWidget(this);

    Widget->setCurrentDirectory(QDir::currentPath());
    this->setCentralWidget(Widget);

    readSettings();

    // menu "Connection"
    connect(ui.actionNew_Folder, SIGNAL(triggered()), this, SLOT(NewFolder()));
    connect(ui.actionNew_Connection, SIGNAL(triggered()), this, SLOT(NewConnection()));
    connect(ui.actionOpen, SIGNAL(triggered()), this, SLOT(OpenConnection()));
    connect(ui.actionDelete, SIGNAL(triggered()), this, SLOT(DeleteConnection()));
    connect(ui.actionProperties, SIGNAL(triggered()), this, SLOT(Properties()));
    connect(ui.actionExit, SIGNAL(triggered()), qApp, SLOT(quit()));
    // menu "View"
    connect(ui.actionFolder_Tree, SIGNAL(triggered()), this, SLOT(ViewFolderTree()));
    // menu "?"
    connect(ui.actionAbout, SIGNAL(triggered()), this, SLOT(About()));
    connect(ui.actionAbout_Qt, SIGNAL(triggered()), qApp, SLOT(aboutQt()));
}

MAINWINDOW::~MAINWINDOW()
{


}

void MAINWINDOW::closeEvent(QCloseEvent *event)
{
    writeSettings();
    event->accept();
}

void MAINWINDOW::NewFolder()
{

}

void MAINWINDOW::NewConnection()
{

}

void MAINWINDOW::OpenConnection()
{

}

void MAINWINDOW::DeleteConnection()
{

}

void MAINWINDOW::Properties()
{

}

void MAINWINDOW::ViewChanged()
{

}

void MAINWINDOW::ViewFolderTree()
{

}

void MAINWINDOW::About()
{
   QMessageBox::about(this, tr("About Application"),
                      tr("The <b>LanExchange</b> is a cool program."));
}

void MAINWINDOW::readSettings()
{
    QSettings settings;
    QSize size = settings.value("size", QSize(800, 400)).toSize();
    resize(size);
    //QSize desktopSize = qApp->desktop()->size();
    //QPoint pos = QPoint(desktopSize.width() - this->width(), desktopSize.height() - this->height());
    //move(pos);
}

void MAINWINDOW::writeSettings()
{
    QSettings settings;
    settings.setValue("size", size());
}
