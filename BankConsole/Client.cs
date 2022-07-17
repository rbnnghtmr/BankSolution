namespace BankConsole;

public class Client : User{

    public char TaxRegime { get; set; }

    public Client(){

    }

    public Client(int ID, string Name, string Email, decimal Balance, char TaxRegime) : base(ID, Name, Email, Balance){
           
            this.TaxRegime = TaxRegime;
            SetBalance(Balance);
    }

    public override void SetBalance(decimal amount)
    {
        base.SetBalance(amount);
            
            if(TaxRegime.Equals('M'))
                Balance += (amount * 0.02m);

    }

    public override string ShowData()
    {
        return base.ShowData() + $", Régimen Fiscal: {this.TaxRegime}";

    }

}