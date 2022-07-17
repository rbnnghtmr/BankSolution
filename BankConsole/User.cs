using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace BankConsole;


public class User{
    [JsonProperty]
    protected int ID{ get; set; }
    [JsonProperty]
    protected string Name{ get; set;}
    [JsonProperty]
    protected string Email{ get; set;}
    [JsonProperty]
    protected decimal Balance { get; set; }
    [JsonProperty]
    private DateTime RegisterDate{ get; set;}

    public User(){

    }

    public int GetID(){
        return ID;
    }
    public User(int ID, string Name, string Email, decimal Balance){

        this.ID = ID;
        this.Name = Name;
        this.Email = Email;
        this.RegisterDate = DateTime.Now;
    }

    public DateTime GetRegisterDate(){
        return this.RegisterDate;
    }

    public virtual void SetBalance(decimal amount){
        decimal quantity = 0;
        
        if(amount < 0 ){
            quantity = 0;
        }else{
            quantity = amount;
        }
        this.Balance += quantity;




    }

    public virtual string ShowData(){

        return $"ID: {this.ID}, Nombre: {this.Name}, Correo: {this.Email}, Saldo: {this.Balance}, Fecha de registro: {this.RegisterDate.ToShortDateString}";
    
    }

    public string ShowData(string initialMessage){
        return $"{initialMessage} -> Nombre: {this.Name}, Correo: {this.Email}, Saldo: {this.Balance}, Fecha de registro: {this.RegisterDate}";

    }

    public static bool IsValidEmail(string email)
    {
            string emailFormat = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if(Regex.IsMatch(email, emailFormat)){
                if(Regex.Replace(email, emailFormat, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }


    }

    public static string IsIntID(int ID)
    {   
        int validInt = ID;
        if (validInt % 1 == 0)
        {
            return "Is int";
        }
        else
        {
            return "Is not int";
        }
    }

        public static string IsDecimalBalance(decimal balance)
    {
        try
        {
            decimal validDecimal = Convert.ToDecimal(balance);
            return "Is decimal";
        }
        catch (Exception)
        {
            return "Is not decimal";
        }
    }
}