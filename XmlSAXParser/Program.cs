using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;

namespace XmlSAXParser
{
    class Program
    {
        [STAThreadAttribute]
        static void Main(string[] args)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.DefaultExt = ".xml";
            openDialog.Filter = "XML file (*.xml)|*.xml";
            openDialog.ShowDialog();

            string path = openDialog.FileName;
            Console.WriteLine("Your xml file is opened on the way {0} ", path);
            Console.WriteLine();


            string mes = "Enter the number of skills, please";
            Console.WriteLine(mes);


            try
            {
                int num = Int32.Parse(Console.ReadLine());
                int countPeople = 0, countSkills = 0;

                using (XmlTextReader reader = new XmlTextReader(path))    
                {
                    reader.WhitespaceHandling = WhitespaceHandling.None;
                    bool toCountEmail = false, toCountSkills = false, inEmail = false;
                    while (reader.Read())
                    {                      
                        if (reader.NodeType == XmlNodeType.EndElement)
                            if (reader.LocalName == "user")
                            {                            
                                toCountSkills = (countSkills > num);
                                countSkills = 0;
                                if (toCountEmail && toCountSkills)
                                {
                                    countPeople++;
                                }
                                toCountEmail = toCountSkills = false;
                            }
                                         
                        if (reader.NodeType == XmlNodeType.Element)
                        {                           
                            if (reader.LocalName == "skill")
                            {
                                countSkills++;
                            }                           
                            inEmail = (reader.LocalName == "email");
                        }

                        if (reader.NodeType == XmlNodeType.Text && inEmail)
                        {                                
                            string email = reader.Value;
                            toCountEmail = (email.EndsWith(".biz"));
                        }                       
                    }   
                }
                Console.WriteLine(countPeople);
              }
              catch (Exception ex)
              {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
        }
    }
}
