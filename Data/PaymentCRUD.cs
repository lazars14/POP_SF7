using System.Collections.Generic;

namespace POP_SF7
{
    class PaymentCRUD
    {
        public static void addPayment(List<Payment> paymentsList, Payment payment)
        {
            paymentsList.Add(payment);
        }

        public static bool deletePayment(List<Payment> paymentsList, int id)
        {
            for(int i = 0; i < paymentsList.Count; i++)
            {
                if(paymentsList[i].Id == id)
                {
                    paymentsList[i].Deleted = true;
                    return true;
                }
            }
            return false;
        }

        public static void editPayment(List<Payment> paymentsList, Payment editedPayment)
        {
            for(int i = 0; i < paymentsList.Count; i++)
            {
                if(paymentsList[i].Id == editedPayment.Id)
                {
                    paymentsList[i] = editedPayment;
                }
            }
        }

        public static void sortPayments(School school)
        {
            List<Payment> list = school.ListOfPayments;
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[i].Id > list[j].Id)
                    {
                        var tmp = list[i];
                        list[i] = list[j];
                        list[j] = tmp;
                    }
                }
            }
        }

        public static Payment findPaymentById(List<Payment> paymentsList, int id)
        {
            foreach(Payment payment in paymentsList)
            {
                if(payment.Id == id)
                {
                    return payment;
                }
            }

            return null;
        }

        public static void find(List<Payment> paymentsList, int userInput, string param, List<string> results)
        {
            foreach (Payment payment in paymentsList)
            {
                switch (param)
                {
                    case "Kurs":
                        /*if(payment.CourseId == userInput)
                        {
                            results.Add(payment.ToString());
                        }*/
                        break;
                    case "Ucenik":
                        /*if(payment.Student == userInput)
                        {
                            results.Add(payment.ToString());
                        }*/
                        break;
                }
            }
        }

        public static int getMaxId(List<Payment> listOfPayments)
        {
            int maxId = 0;
            foreach (Payment payment in listOfPayments)
            {
                if (payment.Id > maxId)
                {
                    maxId = payment.Id;
                }
            }
            maxId += 1;
            return maxId;
        }

    }
}
