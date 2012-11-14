#include "mainwindow.h"
#include <QtGui>

MAINWINDOW::MAINWINDOW(QWidget *parent, Qt::WFlags flags) : QMainWindow(parent, flags)
{
	ui.setupUi(this);
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
