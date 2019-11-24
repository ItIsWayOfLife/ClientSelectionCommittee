using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClientSelectionCommittee
{
    [Serializable]
    public class TrainingDirectionSend
    {
        public int Id { get; set; }
        public string NameBudgetOrCharge { get; set; }
        public string NameFormStudy { get; set; }
        public string NameTrainingPeriod { get; set; }
        public string CodeSpecialty { get; set; }
        public string CodeSpecialization { get; set; }
        public string FullNameSpecialty { get; set; }
        public string ShortNameSpecialty { get; set; }
        public string FullNameFaculty { get; set; }
        public string ShortNameFaculty { get; set; }
        public string FullNameDepartment { get; set; }
        public string ShortNameDepartment { get; set; }
        public string FirstIdEntranceTests { get; set; }
        public string SecondEntranceTests { get; set; }
        public string ThirdEntranceTests { get; set; }
        public int AdmissionPlan { get; set; }

        // запись в xml
        public static void WriteToXml(string words)
        {
            File.Delete("DeserializeFile/TrainingDirectionSend.xml");
            using (StreamWriter writer = new StreamWriter("DeserializeFile/TrainingDirectionSend.xml"))
            {
                writer.Write(words);
            }
        }

        public static List<TrainingDirectionSend> DeserializeFileXml()
        {
            using (FileStream fs = new FileStream("DeserializeFile/TrainingDirectionSend.xml", FileMode.Open))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<TrainingDirectionSend>));

                return (List<TrainingDirectionSend>)formatter.Deserialize(fs);
            }
        }
    }
}
