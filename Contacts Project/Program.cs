using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Contacts_Project
{
    class Program
    {
        struct Contacts
        {
           public string FirstName;
            public string LastName;
            public string PhoneNumber;
            public string Email;
        }

        static void Main(string[] args)
        {



            bool KeepLooping = true;
            string SearchNameVariable = "";
            int UserInput = 0;
            string path= ("c:\\Temp\\Contacts.cnt");
            //loop that keeps the main function running until you decide to exit
            while (KeepLooping){  
            Console.WriteLine("I will hold your contact information because I know it is hard to remember!\n\nPlease enter a option of 0-7.");
            Console.WriteLine("=============================");
            Console.WriteLine("0.Add a new record\n1.Search for a name.\n2.Modify a phone number.\n3.Modify an email.\n4.Delete a record.\n5.List all Contacts\n6.Alphabatize contacts.\n7.Exit the program.");
           
                //get the user input which controls the menu 
            UserInput = int.Parse(Console.ReadLine());



            if(UserInput == 0)
            {
                    AddNewRecord(path);
                    Console.ReadKey();
                    Console.Clear();

            }else if(UserInput == 1)
            {   
                    Console.WriteLine("What name are you searching for?");
                    SearchNameVariable = Console.ReadLine();
                    SearchName(path, SearchNameVariable);
                    Console.ReadKey();
                    Console.Clear();

            }else if(UserInput == 2)
            {
                   
                    ModifyNum(path, CountFile(path));
                    Console.ReadKey();
                    Console.Clear();

            }else if(UserInput == 3)
            {
                    ModifyEmail(path, CountFile(path));
                    Console.ReadKey();
                    Console.Clear();

            }else if(UserInput == 4)
            {
                    DeleteContact(path, CountFile(path));
                    Console.ReadKey();
                    Console.Clear();
            }else if(UserInput == 5)
            {
                    ListContacts(path);
                    Console.ReadKey();
                    Console.Clear();

            }
            else if(UserInput == 6)
            {
                    Alphabetize(path, CountFile(path));
                    Console.ReadKey();
                    Console.Clear();
            }else if(UserInput == 7)
                {
                    Console.ReadKey();
                    Console.Clear();
                    KeepLooping = false;
                }
            Console.ReadKey();
            }
        }

        //Function- Prompts user to input the information requried to store a new contact, then writes that contact directly to the file via the ...
        //...streamwriter file-path
        static void AddNewRecord(string path)
        {
            Contacts newRecord;
            Console.WriteLine("Enter first name: ");
            newRecord.FirstName = Console.ReadLine();
            Console.WriteLine("Enter last name: ");
            newRecord.LastName = Console.ReadLine();
            Console.WriteLine("Enter phone number: ");
            newRecord.PhoneNumber = Console.ReadLine();
            Console.WriteLine("Enter email: ");
            newRecord.Email = Console.ReadLine();

            StreamWriter in_file = File.AppendText(path);
            in_file.Write(newRecord.FirstName+ ",");
            in_file.Write(newRecord.LastName+ ",");
            in_file.Write(newRecord.PhoneNumber+ ",");
            in_file.Write(newRecord.Email);
            in_file.WriteLine();
            in_file.Close();


        }

        //Function- Allows user to input a name, program searches contacts for that name...
        //...if name is found in contacts file then the program will return all information found on that contact 
        static void SearchName (string tempPath, string TempSearchName) {
            string Text = "";
            string[] SplitText;  
            Contacts[] ContactArray = new Contacts [CountFile(tempPath)]; 
          
            StreamReader in_file = new StreamReader(tempPath);
            while (!in_file.EndOfStream) {
                Text = in_file.ReadLine();
                SplitText = Text.Split(',');
   
               if (TempSearchName == SplitText[0]){
                    Console.WriteLine(SplitText[0]+ ","+SplitText[1]+ ","+SplitText[2]+ ","+SplitText[3]);
               }//end if
            }//end while
            in_file.Close();
        }//end function

        //This is another way to approach searching for a name
        static void OtherSearchName (string tempPath, string TempSearchName , int contactcount) {
            string Text = "";
            string[] SplitText;  
            Contacts[] ContactArray = new Contacts [CountFile(tempPath)];
                      //  int contactcount = 0;

         
             StreamReader in_file = new StreamReader(tempPath);
          
             while (!in_file.EndOfStream) {
                 Text = in_file.ReadLine();
                 SplitText = Text.Split(',');
          
                 ContactArray[contactcount].FirstName   = SplitText[0];        
                 ContactArray[contactcount].LastName    = SplitText[1];        
                 ContactArray[contactcount].PhoneNumber = SplitText[2];        
                 ContactArray[contactcount].Email       = SplitText[3];        
                 contactcount += 1;
             }//end while
          
             foreach(Contacts curr_contact in ContactArray){
                 if (curr_contact.FirstName  == TempSearchName){
                     Console.Write(curr_contact.FirstName + " ");
                     Console.Write(curr_contact.LastName + " ");
                     Console.Write(curr_contact.PhoneNumber + " ");
                     Console.WriteLine(curr_contact.Email + " ");
                 }//end if
             }
             in_file.Close();
        } //endregion

        static void ModifyNum ( string tempPath, int ContactCount)
        {
            Contacts[] AllData = new Contacts [ContactCount];
            StreamReader in_file = new StreamReader(tempPath);
            Console.WriteLine("Enter first name of the contact who's number you would like to modify: ");
            string userInputFirstName = Console.ReadLine();
            Console.WriteLine("Enter last name of the contact who;s number you would like to modify: ");
            string userInputLastName = Console.ReadLine();
            int index = 0;

            while (!in_file.EndOfStream)
            {              
                string data = in_file.ReadLine();
                string [] contacts = data.Split(',');

                AllData [index].FirstName = contacts[0];
                AllData [index].LastName = contacts[1];
                AllData [index].PhoneNumber = contacts[2];
                AllData [index].Email = contacts[3];         
            
                if(userInputFirstName == AllData[index].FirstName && userInputLastName== AllData[index].LastName)
                {
                    Console.WriteLine("Enter the new phone number for " + AllData[index].FirstName + " " + AllData[index].LastName);
                    AllData[index].PhoneNumber = Console.ReadLine();                                    
                }
                 index++;                            
            }
            in_file.Close();
            
            StreamWriter out_file = new StreamWriter (tempPath);
            for (int i = 0; i < AllData.Length; i++)
            {
                out_file.WriteLine(AllData[i].FirstName + "," + AllData[i].LastName + "," + AllData[i].PhoneNumber + "," + AllData[i].Email);

            }
            out_file.Close();
        }

        static void ModifyEmail (string tempPath, int ContactCount)
        {
            Contacts[] AllData = new Contacts [ContactCount];
            StreamReader in_file = new StreamReader(tempPath);
            Console.WriteLine("Enter first name of the contact who's number you would like to modify: ");
            string userInputFirstName = Console.ReadLine();
            Console.WriteLine("Enter last name of the contact who;s number you would like to modify: ");
            string userInputLastName = Console.ReadLine();
            int index = 0;

            while (!in_file.EndOfStream)
            {              
                string data = in_file.ReadLine();
                string [] contacts = data.Split(',');

                AllData [index].FirstName = contacts[0];
                AllData [index].LastName = contacts[1];
                AllData [index].PhoneNumber = contacts[2];
                AllData [index].Email = contacts[3];         
            
                if(userInputFirstName == AllData[index].FirstName && userInputLastName== AllData[index].LastName)
                {
                    Console.WriteLine("Enter the new phone number for " + AllData[index].FirstName + " " + AllData[index].LastName);
                    AllData[index].Email = Console.ReadLine();                                    
                }
                 index++;                            
            }
            in_file.Close();

            StreamWriter out_file = new StreamWriter (tempPath);
            for (int i = 0; i < AllData.Length; i++)
            {
                out_file.WriteLine(AllData[i].FirstName + "," + AllData[i].LastName + "," + AllData[i].PhoneNumber + "," + AllData[i].Email);

            }
            out_file.Close();
        }
                
        //Function- creates a counter which allows you to count and number the sections of your contacts file so you can act upon the numbered sections 
        static int CountFile ( string tempPath)
        {
            int ContactCount = 0;
            StreamReader in_file = new StreamReader (tempPath);
            while(!in_file.EndOfStream){
                in_file.ReadLine();
                ContactCount++;
            }
            in_file.Close();
            return ContactCount;
        }
                
        //Fuction- Prints the entire list of your contacts to the console 
        static void ListContacts (string tempPath)
        {            
           string Line = "";
           StreamReader in_file = new StreamReader(tempPath);

            while (!in_file.EndOfStream)
            {
                 Line = in_file.ReadLine();   
                Console.WriteLine(Line);
            }
            in_file.Close();            
        } 

        static void DeleteContact (string tempPath, int ContactCount)
        {
            Contacts[] AllData = new Contacts [ContactCount];
            StreamReader in_file = new StreamReader(tempPath);
            Console.WriteLine("Enter first name of the contact who's number you would like to delete: ");
            string userInputFirstName = Console.ReadLine();
            Console.WriteLine("Enter last name of the contact who;s number you would like to delete: ");
            string userInputLastName = Console.ReadLine();
            int index = 0;
            string [] contacts = new string [ContactCount];

            while (!in_file.EndOfStream)
            {              
                string data = in_file.ReadLine();
                contacts  = data.Split(',');

                AllData [index].FirstName = contacts[0];
                AllData [index].LastName = contacts[1];
                AllData [index].PhoneNumber = contacts[2];
                AllData [index].Email = contacts[3]; 
                index++;
            }
            in_file.Close();
            StreamWriter out_file = new StreamWriter (tempPath);
            for (int i = 0; i < AllData.Length; i++)
            {
                 if (userInputFirstName != AllData[i].FirstName || userInputLastName != AllData[i].LastName)
                {
                    out_file.WriteLine(AllData[i].FirstName + "," + AllData[i].LastName + "," + AllData[i].PhoneNumber + "," + AllData[i].Email);
                } 
            }
            out_file.Close();
        }
        
        static int array_Find_Min_Index(Contacts[] array, int StartingPoint) {
           int smallest = StartingPoint;
           for (int i= StartingPoint; i < array.GetLength(0); i += 1) {
               if (stringCompare(array[i].FirstName, array[smallest].FirstName, true)) {
               smallest = i;
               }
           }
           return smallest;
        }//END FUNCTION
        
        static void Alphabetize(string tempPath, int count) {
                Contacts[] contactData = new Contacts[count];
                StreamReader in_file = new StreamReader(tempPath);
                int index =0;
                while (!in_file.EndOfStream) {
                    string data = in_file.ReadLine();
                    string[] contacts = data.Split(',');
                          contactData[index].FirstName = contacts[0];
                          contactData[index].LastName = contacts[1];
                          contactData[index].PhoneNumber = contacts[2];
                          contactData[index].Email = contacts[3];
                          index++;
                }//End while
                in_file.Close();
                         InsertionSort(contactData);
                
                    StreamWriter Replace = new StreamWriter(tempPath);
            foreach (Contacts temp in contactData)  {
                Replace.WriteLine("{0},{1},{2},{3}", temp.FirstName, temp.LastName, temp.PhoneNumber, temp.Email);
            }
            Replace.Close();
            ListContacts(tempPath);
            }//END FUNCTION
        
        static void SwapArray(int i, int j, Contacts[] ary) {
               Contacts temp;
               if (i != j) {
                temp = ary[i];
                ary[i] = ary[j];
                ary[j] = temp;
                }
            }//END FUNCTION

        static bool stringCompare(string str1, string str2, bool ignoreCase) {
                       //good function for comparing strings 
                       int maxSize = Math.Min(str1.Length, str2.Length);
       
                       if (ignoreCase) {
                           str1 = str1.ToLower();
                           str2 = str2.ToLower();
                       }
               //   str1.CompareTo(str2) < 0;
                       if (str1 == str2) {
                           return false;
                       }
                       for (int index = 0; index < maxSize; index++) {
                           char l1 = str1[index];
                           char l2 = str2[index];
                           if (l1 > l2) { return false; }
                           if (l1 < l2) { return true; }
                       }//END FOR
                       if (str1.Length > str2.Length) {
                           return false;
                       } else if (str1.Length < str2.Length) {
                           return true;
                       }//END IF
                       return false;
                   }

        static void InsertionSort(Contacts[] array) {
           int minIndex = 0;
           for (int i = 0; i < array.GetLength(0); i++)
           {
               minIndex = array_Find_Min_Index(array, i);
               if (minIndex != i)
               {
                   SwapArray(minIndex, i, array);
               }
           }
       }
    }    
}
