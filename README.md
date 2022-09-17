# Humble.PathFinder.UnzipRename

A simple command line applicaiton to complement the [Pathfiner 2E Humbe Bundle]() and other Paizo Downloads.  This application works to take the Paizo
names and simplify them to a more human readable version once the zip files are un-zipped.  This does require the user to already posess the \*.zip files
and to have space to unzip them onto the drive. 

> **Note**<br/>
> this applicaiton does not delete the zip files, but will delete the destination folder if there is a redone.  **Always use a temporary drive location
> as the destination folder**. 

## Using
This application can be ran from the Windows command line tool, either Command Prompt or PowerShell 

``` batch
paizouz.exe [source-dir] [destination-dir]
```
`[source-dir]` _optional_ is the folder containing the zip files, this is not the zip folder it self.

`[destination-dir]` _optional_ is the folder location to which all of the files will be unzipped to with a human readable name.

## 
This code is availbe free of charge and free to distribute, change or alter.  It is not supported, and anyone using it takes all risks to be used. 
