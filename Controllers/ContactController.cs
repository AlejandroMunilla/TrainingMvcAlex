using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TrainingAlex.Helpers;
using TrainingAlex.Models;

namespace TrainingAlex.Controllers
{
    public class ContactController : BaseController
    {
        public IActionResult Index(ContactModel model)
        {
            if (model == null)
            {
                model = new ContactModel();
            }
            else
            {
                model = Submit(model);
            }

            return View(model);
        }

        public ContactModel Submit(ContactModel contactModel)
        {
            Debug.WriteLine("Submit:" + contactModel.Name + "/" + contactModel.Email);
            bool everythingIsOk = true;
            List<string> errorsList = new List<string>();
            StringValidatorHelper stringValidatorHelper = new StringValidatorHelper();

            if (contactModel.Email == null || contactModel.Email.Length == 0 || !stringValidatorHelper.IsValidEmail(contactModel.Email))
            {
                errorsList.Add(DictionaryHelper.Instance.CheckIfKeyExist("IncorrectEmail"));
                everythingIsOk = false;
            }

            if (String.IsNullOrEmpty(contactModel.Name) || string.IsNullOrWhiteSpace(contactModel.Name))
            {
                errorsList.Add(DictionaryHelper.Instance.CheckIfKeyExist("IncorrectField") + ":" + DictionaryHelper.Instance.CheckIfKeyExist("Name"));
                everythingIsOk = false;
            }

            if (everythingIsOk == true)
            {
                Debug.WriteLine("Ok");
                errorsList.Clear();
                SaveContactDetails(contactModel);
                Response.Redirect("Contact/DisplaySimpleMessage?message=" + SimpleMessage);
                return contactModel;
            }
            else
            {
                foreach (var error in errorsList)
                {
                    Debug.WriteLine(error);
                }
                return contactModel;
            }
        }

        private void SaveContactDetails (ContactModel contactModel)
        {
            //TODO: save to file, db, as json, csv, excel, whaever we want. 
        }
    }
}
