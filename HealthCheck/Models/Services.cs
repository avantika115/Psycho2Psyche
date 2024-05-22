using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthCheck.Models
{
    public class ServicesModel
    {
        public string Name { get; set; }
        public List<Questionaire> Questions { get; set; }
        public int TotalScore { get; set; }
        public string contenttitle { get; set; }
        public string contentdescription { get; set; }
        public string Report { get; set; }
    }

    public class Services
    {
        public ServicesModel Service { get; set; }
        public Services(string ServiceCalled)
        {
            Service = new ServicesModel();
            Service.Name = ServiceCalled;
            Service.contenttitle = "Am I sad or depressed?";
            Service.contentdescription = "Sadness is a normal human emotion whereas depression is a mental health concern that can affect how you think, feel or behave in many ways. Take this quiz to find out whether you are experiencing signs of depression.";
            
        }

        public Services(string ServiceCalled, string testpage)
        {
            Service = new ServicesModel();
            Service.Name = ServiceCalled;
            Service.Questions = getQuestions(ServiceCalled);
        }

        List<Questionaire> getQuestions(string servicecalled)
        {
            return new List<Questionaire>()
            {
                new Questionaire(){
                    QuestionID = 1, Questions = new List<string>(){
                    "I do not feel sad","I feel sad","Iam sad all the time and I can't snap out of it.","I am so sad and unhappy that I can't stand it"
                    }
                },
                new Questionaire(){
                    QuestionID = 2, Questions = new List<string>(){
                    "I am not particularly discouraged about the future","I feel discouraged about the future",
                        "feel I have nothing to look forward to","I feel the future is hopeless and that things cannot improve"
                    }
                },
                new Questionaire(){
                    QuestionID = 3, Questions = new List<string>(){
                    "I do not feel like a failure.",
                        "I feel I have failed more than the average person.",
                        "As I look back on my life, all I can see is a lot of failures.",
                        "I feel I am a complete failure as a person."
                    }
                },
                new Questionaire(){
                    QuestionID = 4, Questions = new List<string>(){
                    "I get as much satisfaction out of things as I used to.",
                        "I don't enjoy things the way I used to.",
                        "I don't get real satisfaction out of anything anymore.",
                        "I am dissatisfied or bored with everything."
                    }
                },
                new Questionaire(){
                    QuestionID = 5, Questions = new List<string>(){
                    "I don't feel particularly guilty",
                    "I feel guilty a good part of the time.",
                    "I feel quite guilty most of the time.",
                    "I feel guilty all of the time."
                    }
                }
            };
        }
    }
}