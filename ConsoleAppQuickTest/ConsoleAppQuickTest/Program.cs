using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleAppQuickTest
{
    class Program
    {
        static void Main(string[] args)
        {
            JiraTicket jt1 = new JiraTicket() { JiraId = "10001", InternalDevId = "-1" };
            JiraTicket jt2 = new JiraTicket() { JiraId = "10002", InternalDevId = "-1" }; // sera dans les WI mais pas de InternalDevId renseigné
            JiraTicket jt3 = new JiraTicket() { JiraId = "10003", InternalDevId = "-1" };
            JiraTicket jt4 = new JiraTicket() { JiraId = "10004", InternalDevId = "-1" };
            JiraTicket jt5 = new JiraTicket() { JiraId = "10005", InternalDevId = "B" }; //  InternalDevId a été renseigné depuis
            JiraTicket jt6 = new JiraTicket() { JiraId = "10006", InternalDevId = "-1" };
            JiraTicket jt7 = new JiraTicket() { JiraId = "10007", InternalDevId = "-1" };
            JiraTicket jt8 = new JiraTicket() { JiraId = "10008", InternalDevId = "A" }; //  InternalDevId a été renseigné depuis
            JiraTicket jt9 = new JiraTicket() { JiraId = "10009", InternalDevId = "C" }; //  InternalDevId a été rectifié depuis
            JiraTicket jt10 = new JiraTicket() { JiraId = "100010", InternalDevId = "D" }; // sera dans les WI mais le InternalDevId n'a pas bougé
            List<JiraTicket> listeJiraJiraTickets = new List<JiraTicket>()
            {
                jt1, jt2, jt3, jt4, jt5, jt6, jt7, jt8, jt9, jt10
            };


            AzdTicket wi1 = new AzdTicket() { AzdId = "202", JiraId = "111", JiraInternalDevId = "-1" };
            AzdTicket wi2 = new AzdTicket() { AzdId = "203", JiraId = "10008", JiraInternalDevId = "-1" };
            AzdTicket wi3 = new AzdTicket() { AzdId = "204", JiraId = "111", JiraInternalDevId = "-1" };
            AzdTicket wi4 = new AzdTicket() { AzdId = "205", JiraId = "10005", JiraInternalDevId = "-1" };
            AzdTicket wi5 = new AzdTicket() { AzdId = "206", JiraId = "111", JiraInternalDevId = "-1" };
            AzdTicket wi6 = new AzdTicket() { AzdId = "207", JiraId = "10002", JiraInternalDevId = "-1" };
            AzdTicket wi7 = new AzdTicket() { AzdId = "208", JiraId = "10009", JiraInternalDevId = "ZZ" };
            AzdTicket wi8 = new AzdTicket() { AzdId = "209", JiraId = "100010", JiraInternalDevId = "D" };

            List<AzdTicket> listeAzdTickets = new List<AzdTicket>()
            {
                wi1, wi2, wi3, wi4, wi5, wi6, wi7, wi8
            };

            var listeWICandidatsAMettreAJour = listeAzdTickets.Where(wi => listeJiraJiraTickets.Any(j => j.JiraId.Equals(wi.JiraId)));


            foreach (var wi in listeWICandidatsAMettreAJour)
            {
                Console.WriteLine($"\nWI à mettre à jour potentiellement (?) : {wi}");
                
                string curAssociateJiraInternalDevId = listeJiraJiraTickets.SingleOrDefault(jt => jt.JiraId.Equals(wi.JiraId))?.InternalDevId;
                if (curAssociateJiraInternalDevId != null 
                    && curAssociateJiraInternalDevId != "-1" /* ou bien: != 0*/    // InternalDevId bien renseigné depuis JIRA
                    && curAssociateJiraInternalDevId != wi.JiraInternalDevId)      // JiraInternalDevId est différent de celui du JIRA
                {
                    // 1. Logger qu'on va faire un update du JiraInternalDevId

                    // 2. Appeler API AZURE DEVOPS avec le WI pour mettre à jour le champ JiraInternalDevId
                    // https://docs.microsoft.com/en-us/rest/api/azure/devops/wit/work-items/update?view=azure-devops-rest-6.0#update-a-field
                    // exemple https://dev.azure.com/fabrikam/_apis/wit/workitems/{id}?api-version=6.0 avec op = replace pour le champ JiraInternalDevId

                    wi.JiraInternalDevId = curAssociateJiraInternalDevId;
                    Console.WriteLine($"\t ------> WI mise à jour effective OK : {wi}");
                }
            }

            Console.WriteLine("Push to exit");
            Console.Read();
        }
    }
}
