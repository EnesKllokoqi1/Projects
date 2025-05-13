using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Net;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
namespace Program
{
    class ConsoleApp2
    {
        static async Task Main(string[] args){
            string folderName=null;
            string rootPath=AppDomain.CurrentDomain.BaseDirectory;
            FolderMaker folderMaker = new(rootPath);
            string heading=" Note System : ";
            System.Console.WriteLine(heading);
            System.Console.WriteLine(new string('-',heading.Length));
            while(true){
                System.Console.WriteLine("Enter 1 to make a Folder : ");
                System.Console.WriteLine("Enter 2 to make a File for the Folder :  ");
                System.Console.WriteLine("Enter 3 to write content to the File : ");
                System.Console.WriteLine("Enter 4 to update the content of the File : ");
                System.Console.WriteLine("Enter 5 to remove the File from the Folder ");
                System.Console.WriteLine("Enter 6 to view Files: ");
                System.Console.WriteLine("Enter 7 to delete the Folder : ");
                System.Console.WriteLine();
                int response=Convert.ToInt32(Console.ReadLine());
                switch(response){
                    case 1:
                     if(!string.IsNullOrEmpty(folderName)){
                        System.Console.WriteLine("A folder is already created u cannot create another one .");
                        break;
                    }
                    System.Console.WriteLine("Enter the name of the folder");
                     folderName=Console.ReadLine();
                     folderMaker.Folder=folderName;
                    if(!string.IsNullOrEmpty(folderName)){
                    await folderMaker.CreateFolder(folderName);
                    }else{
                        System.Console.WriteLine("U cant create a empty folder");
                    }
                    break;
                    case 2:
                     if (string.IsNullOrEmpty(folderName))
                       {
                       Console.WriteLine("No folder selected. Please create a folder first.");
                       break;
                      }
                    System.Console.WriteLine($"Enter a file for the folder :{folderName} ");
                    string fileName1=Console.ReadLine();
                    fileName1+=".txt";//always a text file no binary stuff
                    if(!string.IsNullOrEmpty(fileName1)){
                   await folderMaker.CreateFile(fileName1);
                    }else{
                        System.Console.WriteLine("Cannot create a empty file.");
                    }
                    break;
                    case 3: 
                      if (string.IsNullOrEmpty(folderName))
                       {
                       Console.WriteLine("No folder selected. Please create a folder first.");
                       break;
                      }
                    System.Console.WriteLine("Current files  :");
                         DirectoryInfo directoryInfos1=await folderMaker.FolderData(folderName);
                         FileInfo[] fileInfos1 = directoryInfos1.GetFiles("*.txt"); 
                         foreach(var file in fileInfos1){
                            System.Console.WriteLine();
                            System.Console.WriteLine(file.Name);
                            System.Console.WriteLine();
                         }
                         System.Console.WriteLine("Enter the file u want to write content to :");
                         string fileContent=Console.ReadLine();
                         bool fileExists = fileInfos1.Any(f => f.Name.Equals(fileContent, StringComparison.OrdinalIgnoreCase));
                         if(fileExists && !string.IsNullOrEmpty(fileContent)){
                          System.Console.WriteLine($"Please enter some content for file name : {fileContent} :");
                        var input=Console.ReadLine();
                     if(!string.IsNullOrWhiteSpace(input)){
                      await  folderMaker.WriteFileContent(input,fileContent);
                     }
                     else{
                        System.Console.WriteLine("Dont enter empty content pls.");
                        break;
                     }
                     }else{
                        System.Console.WriteLine("File doesnt exist.");
                     }
                    break;
                    case 4: 
                     if (string.IsNullOrEmpty(folderName))
                       {
                       Console.WriteLine("No folder selected. Please create a folder first.");
                       break;
                      }
                      
                    System.Console.WriteLine($"Files for the folder : {folderName}");
                      System.Console.WriteLine(new string('-',heading.Length));
                    DirectoryInfo  directoryInfo =await folderMaker.FolderData(folderName);
                    FileInfo[] fileInfos2 = directoryInfo.GetFiles("*.txt");
                    for(int i=0;i<fileInfos2.Length;i++){
                        System.Console.WriteLine($"File ID : {i+1} {fileInfos2[i].Name}");
                    }
                    System.Console.WriteLine("Enter which file to update its content to by accessing its ID : ");
                    int Id=Convert.ToInt32(Console.ReadLine());
                    if(Id<=0 || Id>fileInfos2.Length){
                        await Task.Delay(1000);
                        System.Console.WriteLine("Invalid ID input.");
                        break;
                    }
                    System.Console.WriteLine("Enter new content to be updated : ");
                    string content=Console.ReadLine();
                    if(!string.IsNullOrWhiteSpace(content)){
                    string specificFile=fileInfos2[Id-1].FullName;
                    await folderMaker.UpdateFileContent(specificFile,content);
                    }else{
                        System.Console.WriteLine("No empty content pls");
                    }
                    break;
                    case 5:
                    if(string.IsNullOrEmpty(folderName)){
                        await Task.Delay(1000);
                        System.Console.WriteLine("Folder is not created.");
                        break;
                    }
                    System.Console.WriteLine($"Files for  the folder : {folderName}");
                    System.Console.WriteLine(new string('-',heading.Length));
                    System.Console.WriteLine();
                    DirectoryInfo directoryInfos=await folderMaker.FolderData(folderName);
                    FileInfo[] fileInfos = directoryInfos.GetFiles("*.txt");
                    if(fileInfos.Length==0){
                        await Task.Delay(1000);
                        System.Console.WriteLine($"No files are added in the folder : {folderName} ");
                        break;
                    }
                    for(int i=0;i<fileInfos.Length;i++){
                        System.Console.WriteLine($"File {i+1}  :{fileInfos[i]}");
                    }
                    System.Console.WriteLine("Enter file number to remove its file : ");
                    int fileNumber=Convert.ToInt32(Console.ReadLine());
                    if(fileNumber<0 || fileNumber>fileInfos.Length){
                        await Task.Delay(1000);
                        System.Console.WriteLine("Invalid File number input.");
                        break;
                    }
                    await folderMaker.DeleteFile(fileInfos,fileNumber);
                    break;
                    case 6:
                     if(string.IsNullOrEmpty(folderName)){
                        await Task.Delay(1000);
                        System.Console.WriteLine("Folder is not created.");
                        break;
                    }
                    DirectoryInfo directoryInfo1 = await folderMaker.FolderData(folderName);
                    FileInfo[] fileInfos3 = directoryInfo1.GetFiles("*.txt");
                    for(int i=0;i<fileInfos3.Length;i++){
                        System.Console.WriteLine($"File Id : {i+1} file : {fileInfos3[i].Name}");
                    }
                    System.Console.WriteLine("Enter file Id to view its contents or press a to view all of the files contents . ");
                    string viewResponse=Console.ReadLine();
                    if(int.TryParse(viewResponse,out int IdFile) && IdFile>=1 && IdFile<=fileInfos3.Length ){
                        string fileContents=await File.ReadAllTextAsync(fileInfos3[IdFile-1].FullName);
                        if(fileContents!=null){
                        await folderMaker.ViewSpecificFileContents(fileInfos3,IdFile);
                        }else{
                            System.Console.WriteLine($"File exists  {fileInfos3[IdFile-1].Name} but no content is written to it ");
                            break;
                        }
                    }else if(viewResponse.ToLower()=="a"){
                       await folderMaker.ViewAllFileContents(fileInfos3);
                    }
                    else{
                        System.Console.WriteLine("Invalid  input.");
                        break;
                    }
                    break;
                    case 7:
                     if(string.IsNullOrEmpty(folderName)){
                        await Task.Delay(1000);
                        System.Console.WriteLine("Folder is not created.");
                        break;
                    }
                    System.Console.WriteLine("Enter the folder name to delete it : or press D to automaticaly delete the folder.");
                    string folderResponse=Console.ReadLine();
                    if(folderResponse.ToLower()=="d"){
                     await  folderMaker.DeleteFolder();
                     folderName = null;
                    }else if(folderResponse==folderName){
                        await folderMaker.DeleteFolder(folderResponse);
                        folderName = null;
                    }else{
                        System.Console.WriteLine("Invalid input.");
                        break;
                    }
                    break;
                    default:
                    System.Console.WriteLine("Incorrect User input.");
                    break;
                }
            }
        }
    }
    public class FolderMaker{
        private  string createdFile;
        private string RootPath;
        public string Folder{get;set;}
        public FolderMaker(string rootPath){
            RootPath=rootPath;
        }
        public async Task<DirectoryInfo> FolderData(string folder){
            await Task.Delay(1000);
            folder=Path.Combine(RootPath,Folder);
            DirectoryInfo directoryInfo = new DirectoryInfo(folder);
            int numOfFiles=directoryInfo.GetFiles("*.txt").Length;
            if(numOfFiles==0){
                System.Console.WriteLine($"No files are added for  the folder : {Folder}");
            }
            return directoryInfo;
        }

        public async Task CreateFolder(string folderName){
            string SpecifiedFolder=Path.Combine(RootPath,folderName);
            if(!Directory.Exists(SpecifiedFolder)){
                Directory.CreateDirectory(SpecifiedFolder);
                await Task.Delay(1000);
                System.Console.WriteLine("Folder created sucssefuly!!");
            }else{
                System.Console.WriteLine($"The Folder {SpecifiedFolder} already exists enter another one  :");
            }
        }
        public async Task<string> CreateFile(string fileName){
           createdFile=Path.Combine(RootPath,Folder,fileName);
          using(StreamWriter  sw = new StreamWriter(createdFile)){
            await Task.Delay(1000);
            System.Console.WriteLine($"File {fileName} created sucsefuly!!");
          }
            return createdFile;
        }
        public async Task WriteFileContent(string content,string targetedFile){
          string  targetedFile1=Path.Combine(RootPath,Folder,targetedFile);
         using(StreamWriter sw = new StreamWriter(targetedFile1,true)){
          await  sw.WriteLineAsync(content);
          await Task.Delay(1000);
          System.Console.WriteLine($"Content written for file : {targetedFile} is completed !!!!");
          System.Console.WriteLine();
         }
        }
        public async Task UpdateFileContent(string specificFile,string newContent){
            string specificFile1=specificFile;
       await File.WriteAllTextAsync(specificFile1,newContent);
       await Task.Delay(1000);
       System.Console.WriteLine("Content updated sucssefuly!!!");
        }
        public async Task DeleteFile(FileInfo[] fileInfos, int number){
                if(File.Exists(fileInfos[number-1].FullName)){
                    await Task.Delay(1000);
                    File.Delete(fileInfos[number-1].FullName);
                    System.Console.WriteLine("File successfuly deleted!!");
                    System.Console.WriteLine();
                }
        }
        public async Task DeleteFolder(string folderName){
            if(Directory.Exists(folderName)){
            await Task.Delay(1000);
            Directory.Delete(folderName);
            System.Console.WriteLine($"Folder deleted successfully!!");
            }else{
                System.Console.WriteLine("There is no folder to delete ");
            }
        }
          public async Task DeleteFolder(){
            if(Directory.Exists(Folder)){
            await Task.Delay(1000);
            Directory.Delete(Folder);
            System.Console.WriteLine($"Folder deleted successfully!!");
            }else{
                System.Console.WriteLine("There is no folder to delete ");
            }
        }
        public async Task ViewSpecificFileContents(FileInfo[] fileInfos,int choice){
            using (StreamReader streamReader= new StreamReader(fileInfos[choice-1].FullName))
            {
                await Task.Delay(1000);
                System.Console.WriteLine($"File content for {fileInfos[choice-1].Name} :");
              string? content=await streamReader.ReadLineAsync();
              System.Console.WriteLine(content);
            }
        }
          public async Task ViewAllFileContents(FileInfo[] fileInfos){
            for(int i=0;i<fileInfos.Length;i++){
                await Task.Delay(1000);
                string content=await File.ReadAllTextAsync(fileInfos[i].FullName);
                if(content!=null){
                System.Console.WriteLine($"File's Id : {i+1},File's Name : {fileInfos[i].Name} contents");
                System.Console.WriteLine($"----------------------------------------------------");
                using(StreamReader streamReader = new StreamReader(fileInfos[i].FullName)){
                    System.Console.WriteLine();
                    string reader=await streamReader.ReadLineAsync();
                    System.Console.WriteLine(reader);
                    System.Console.WriteLine();
                }
                }else{
                     await Task.Delay(1000);
                    System.Console.WriteLine($"File : {fileInfos[i].Name} exists but no content is in it");
                }
            }
            }
        }

    }
