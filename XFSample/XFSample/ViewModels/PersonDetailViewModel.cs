using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XFSample.Helpers;
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
            _repository = new Repository<Person>();
            _id = id;

            if (_id > 0)
                _ = MapPerson();

            AddValidations();

            SaveCommand = new Command(async () => await Save());
            DeleteCommand = new Command(async () => await Delete());
        }

        private readonly int _id;
        private Person _person;
        public ICommand SaveCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }

        public bool Editable => _id > 0;
        public string Title => Editable ? "Editar" : "Adicionar";

        public ValidatableObject<string> Name { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> PhoneNumber { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Email { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Password { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<DateTime> BirthDate { get; set; }
            = new ValidatableObject<DateTime>() { Value = DateTime.Today };

        private void AddValidations()
        {
            Name.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Obrigatório informar um nome" });

            Email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "O Email deve ser preenchido" });
            Email.Validations.Add(new IsValidEmailRule<string> { ValidationMessage = "O Email é inválido" });

            BirthDate.Validations.Add(new ValidAgeRule<DateTime> { ValidationMessage = "Você deve ter 18 anos ou mais" });

            PhoneNumber.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "O Telefone deve ser preenchido" });
            PhoneNumber.Validations.Add(new MinAndMaxValidRule<string>
            {
                ValidationMessage = "O número do telefone precisar ter entre 9 e 13 digitos",
                MinimumLenght = 9,
                MaximumLenght = 13
            });
        }

        private async Task MapPerson()
        {
            _person = await _repository.GetAsync(_id);

            Name.Value = _person.Name;
            Email.Value = _person.Email;
            PhoneNumber.Value = _person.Phone;
            BirthDate.Value = _person.BirthDate;
            Password.Value = _person.Password;
        }

        private async Task Save()
        {
            try
            {
                await DialogsHelper.ShowLoading("Salvando suas informações");

                if (!CanSave())
                    return;

                _person = new Person(Name.Value, PhoneNumber.Value, Email.Value, Password.Value, BirthDate.Value, _id);
                
                await _repository.SaveAsync(_person);
                await Navigation.PopAsync();
            }
            catch
            {
                await DialogsHelper.ShowToast("Erro ao salvar suas informações.");
            }
            finally
            {
                await DialogsHelper.HideLoading();
            }
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
            try
            {
                var confirm = await DialogsHelper.ShowConfirm("Atenção", "Deseja excluir este registro?");
                if (!confirm) return;

                await _repository.DeleteAsync(_person);
                await Navigation.PopAsync();
            }
            catch
            {
                await DialogsHelper.ShowAlert("Atenção", "Não foi possível excluir o registro");
            }
        }
    }
}
