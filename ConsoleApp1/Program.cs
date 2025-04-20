using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Globalization;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Transactions;
namespace ConsoleApp1{
    class Program2{
        static void Main(string[] args){
            SortedDictionary<int,List<string>> tasks = new SortedDictionary<int, List<string>>();
            while(true){
            System.Console.WriteLine("Press 1 to view tasks : ");
            System.Console.WriteLine("Press 2 to add the ID of a task : ");
            System.Console.WriteLine("Press 3 Add a Task : ");
            System.Console.WriteLine("Press 4 Remove a task : ");
            System.Console.WriteLine("Press 5 to update a task : ");
           int input1;
           if(!int.TryParse(Console.ReadLine(), out input1))
           {
            Console.WriteLine("Invalid input. Please enter a number.");
            continue;
           }
            switch(input1){
                case 1 :
                if(tasks.Count>0){
                foreach(var (key,value) in tasks){
                    if(value!=null && value.Count>0){
                    System.Console.WriteLine();
                    System.Console.WriteLine($"Task ID  : {key}");
                    foreach(var item in value){
                        System.Console.WriteLine($"Task  : {item}");
                    }
                    System.Console.WriteLine();
                    }else{
                        System.Console.WriteLine();
                        System.Console.WriteLine("A ID is entered but no task");
                        System.Console.WriteLine();
                    }
                }
                }
                else{
                    System.Console.WriteLine();
                    System.Console.WriteLine("Tasks are empty u cant remove or view them : ");
                    System.Console.WriteLine("U must add a task ID and also a task for that ID : ");
                    System.Console.WriteLine();
                }
                break;
                case 2 :
                System.Console.WriteLine("Enter task ID : ");
               int number1;
                if(!int.TryParse(Console.ReadLine(),out number1) || number1<=0){
                    System.Console.WriteLine("U must enter a number greater then 0");
                    continue;
                }
                 if(!tasks.ContainsKey(number1)){
                tasks.Add(number1, new List<string>());
                 }else{
                    System.Console.WriteLine("Task ID  is already in the dictionary enter another ID : ");
                    continue;
                 }
                break;
                case 3 :
                    System.Console.WriteLine("Enter the  ID  of task : ");
                    int number2;
                       if(!int.TryParse(Console.ReadLine(),out number2) || number2<=0){
                    System.Console.WriteLine("U must enter a number greater then 0");
                    continue;
                      }
                     if(tasks.ContainsKey(number2)){
                       System.Console.WriteLine($"Add task name for task ID ({number2}) :");
                     }else{
                        System.Console.WriteLine("ID either exists or doesnt exist please try again: ");
                        continue;
                     }
                 string specificTask=Console.ReadLine();
                 if(!string.IsNullOrEmpty(specificTask)){
                if(!tasks[number2].Contains(specificTask)){
                    tasks[number2].Add(specificTask);
                }else{
                    System.Console.WriteLine("Cant add the same value");
                }
                }else{
                    System.Console.WriteLine("You must enter a  valid task name ");
                    continue;
                }
                break;
                case 4:
                if(tasks.Count==0){
                    System.Console.WriteLine("There are no tasks to remove. Please add a task ID and a value after that first. ");
                }else{ 
                      System.Console.WriteLine("Current tasks :");
                      foreach(var (key,val) in tasks){
                        if(val!=null && val.Count>0){
                        System.Console.WriteLine();
                        System.Console.WriteLine($"Task id : {key}");
                        foreach(var item in val )
                        {
                        System.Console.WriteLine($"Task value :  {item}");
                        }
                        System.Console.WriteLine();
                        }
                      }
                    System.Console.WriteLine("Enter task number u want to remove : ");
                    int number3;
                    if(!int.TryParse(Console.ReadLine(),out number3) || number3<=0){
                    System.Console.WriteLine("U must enter a number greater then 0");
                    continue;
                    }
                    if(tasks.ContainsKey(number3)){
                        tasks.Remove(number3);
                        System.Console.WriteLine("Task number removed sucessfuly!");
                        System.Console.WriteLine("Task that is associated with this number is also removed");
                    }
                    else{
                        System.Console.WriteLine();
                        System.Console.WriteLine("Key either doesnt exist or is provided without a value :");
                        System.Console.WriteLine();
                        break;
                    }
                   
                    
                }
                break;
                case 5:
              System.Console.WriteLine("Select which task value to update by accessing its ID: ");
    int number4;
    if (!int.TryParse(Console.ReadLine(), out number4) || number4 <= 0)
    {
        System.Console.WriteLine("You must enter a valid task ID greater than 0");
        continue;
    }

    if (tasks.ContainsKey(number4))
    {
       
        System.Console.WriteLine("Current tasks for this ID:");
        for (int i = 0; i < tasks[number4].Count; i++)
        {
            System.Console.WriteLine($"Task {i + 1}: {tasks[number4][i]}");
        }
        System.Console.WriteLine("Enter the task number you want to update: ");
        int taskNumber;
        if (!int.TryParse(Console.ReadLine(), out taskNumber) || taskNumber <= 0 || taskNumber > tasks[number4].Count)
        {
            System.Console.WriteLine("Invalid task number. Please try again.");
            continue;
        }
        System.Console.WriteLine("Enter the new task value: ");
        string newValue = Console.ReadLine();

        if (!string.IsNullOrEmpty(newValue) && !int.TryParse(newValue,out _))
        {
            tasks[number4][taskNumber - 1] = newValue;
            System.Console.WriteLine("Task updated successfully.");
        }
        else
        {
            System.Console.WriteLine("Task value cannot be empty or a number. Update failed.");
        }
    }
    else
    {
        System.Console.WriteLine("Task ID not found. Please try again.");
    }
     break;    
         }
      }
   }
           
  }
 }
