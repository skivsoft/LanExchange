#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QtGui/QMainWindow>
#include "ui_mainwindow.h"

class MAINWINDOW : public QMainWindow
{
	Q_OBJECT

public:
	MAINWINDOW(QWidget *parent = 0, Qt::WFlags flags = 0);
	~MAINWINDOW();

protected:
    void closeEvent(QCloseEvent *event);

private slots:
    void NewFolder();
    void NewConnection();
    void OpenConnection();
    void DeleteConnection();
    void Properties();
    void ViewChanged();
    void ViewFolderTree();
    void About();

private:
    void readSettings();
    void writeSettings();

	Ui::MainWindow ui;

};


#endif // MAINWINDOW_H
