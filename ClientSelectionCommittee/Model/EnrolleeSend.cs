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
    public class EnrolleeSend
    {
        public int Id { get; set; }
        public int IdDirectionTraining { get; set; }
        public string NameLevelEducation { get; set; }
        public string NameConcession { get; set; }
        public string DescriptionConcession { get; set; }
        public DateTime EnrolleeDateOfRegistration { get; set; }
        public string EnrolleeFirstname { get; set; }
        public string EnrolleeLastname { get; set; }
        public string EnrolleePatronymic { get; set; }
        public string EnrolleeSex { get; set; }
        public DateTime EnrolleeDateOfBirth { get; set; }
        public string EnrolleePassportSeries { get; set; }
        public string EnrolleePassportNumber { get; set; }
        public string EnrolleePassportPersonalNumber { get; set; }
        public string EnrolleePassportIssuedBy { get; set; }
        public DateTime EnrolleeDateOfIssue { get; set; }
        public DateTime EnrolleeDateExpiry { get; set; }
        public string EnrolleeAddress { get; set; }
        public string EnrolleePostcode { get; set; }
        public string EnrolleePhoneNumber { get; set; }
        public string EnrolleeEducation { get; set; }
        public string EnrolleeLastPlaceOfStudy { get; set; }
        public string EnrolleeAddressLastPlaceOfStudy { get; set; }
        public DateTime EnrolleeGraduationDate { get; set; }
        public string EnrolleeNumberCertificateOrDiploma { get; set; }
        public double? EnrolleeAverageGradeOfCertificateOrDiploma { get; set; }
        public double? EnrolleeScoreOfTheFirstEntranceTest { get; set; }
        public double? EnrolleeScoreOfTheSecondEntranceTest { get; set; }
        public double? EnrolleeScoreOfTheThirdEntranceTest { get; set; }
        public string EnrolleeEmail { get; set; }
        public string EnrolleeAdditionalInformation { get; set; }



        // запись в xml
        public static void WriteToXml(string words)
        {
            File.Delete("DeserializeFile/EnrolleeSend.xml");
            using (StreamWriter writer = new StreamWriter("DeserializeFile/EnrolleeSend.xml"))
            {
                writer.Write(words);
            }
        }


        public static List<EnrolleeSend> DeserializeFileXml()
        {
            using (FileStream fs = new FileStream("DeserializeFile/EnrolleeSend.xml", FileMode.Open))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<EnrolleeSend>));

                return (List<EnrolleeSend>)formatter.Deserialize(fs);
            }
        }

    }

}
