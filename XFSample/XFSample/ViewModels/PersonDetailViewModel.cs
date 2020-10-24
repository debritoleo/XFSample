using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XFSample.Infra.Data;
using XFSample.Models;
using XFSample.Services.Validations.Base;
using XFSample.Services.Validations.Rules;
using XFSamples.ViewModels;

namespace XFSample.ViewModels
{
    public class PersonDetailViewModel : BaseViewModel
    {
        private readonly IRepository<Person> _repository;

        public PersonDetailViewModel(INavigation navigation, int id = 0) : base(navigation)
        {
            _repository =
                new Repository<Person>
                (Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "People.db3"));

            _id = id;
            if (_id > 0)
            {
                MapPerson();
            }
            Email = new ValidatableObject<string>();
            AddValidations();
            SaveCommand = new Command(async () => await Save());
            DeleteCommand = new Command(async () => await Delete());
        }

        private int _id;
        private Person _person;
        public ICommand SaveCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }

        public ValidatableObject<string> Email { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Name { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> PhoneNumber { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Password { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<DateTime> DtBirth { get; set; } = new ValidatableObject<DateTime>() { Value = DateTime.Today};

        private void AddValidations()
        {
            Name.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Obrigatório informar um nome" });

            Email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "O Email deve ser preenchido" });
            Email.Validations.Add(new IsValidEmailRule<string> { ValidationMessage = "O Email é inválido" });

            DtBirth.Validations.Add(new ValidAgeRule<DateTime> { ValidationMessage = "Você deve ter 18 anos ou mais" });

            PhoneNumber.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "o Telefone deve ser preenchido" });
        }

        private async Task MapPerson()
        {
            _person = await _repository.GetAsync(_id);

            Name.Value = _person.Name;
            Email.Value = _person.Email;
            PhoneNumber.Value = _person.Phone;
            DtBirth.Value = _person.DtBirth;
        }

        private async Task Save()
        {
            if (!CanSave())
                return;

            _person = new Person(Name.Value, PhoneNumber.Value, Email.Value, Password.Value, DtBirth.Value);
            await _repository.SaveAsync(_person);
        }

        private bool CanSave()
        {
            var props = this.GetType().GetProperties()
                                      .Where(x => x.PropertyType.Name == typeof(ValidatableObject<>).Name);

            foreach (var item in props)
            {
                if (item != null)
                {
                    var validity = item.GetValue(this) as IValidity;

                    if (validity == null)
                        continue;

                    if (!validity.Validate())
                        return false;
                }
            }

            return true;
        }

        private async Task Delete()
        {
            await _repository.DeleteAsync(_person);
            await Navigation.PopAsync();
        }
    }
}
