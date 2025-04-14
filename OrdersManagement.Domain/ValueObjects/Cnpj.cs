namespace OrdersManagement.Domain.ValueObjects
{
    public class Cnpj
    {
		public string Value { get; } = string.Empty;

		private Cnpj() { }
        public Cnpj(string value)
        {
            var cnpj = GetOnlyDigits(value);
            var validCnpj = ValidateCnpj(cnpj);
            
            if (!validCnpj) throw new ArgumentException("CNPJ inválido.", nameof(value));

            Value = value;
        }

        private string GetOnlyDigits(string input) =>
            new string(input.Where(char.IsDigit).ToArray());

        private bool ValidateCnpj(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            
            if (value.Length != 14) return false;

            if (!IsValidCnpj(value)) return false;
            return true;
        }

        private bool IsValidCnpj(string cnpj)
        {
            // Macoratti
            int[] multiplicador1 = [5,4,3,2,9,8,7,6,5,4,3,2];
			int[] multiplicador2 = [6,5,4,3,2,9,8,7,6,5,4,3,2];
			int soma;
			int resto;
			string digito;
			string tempCnpj;
			cnpj = cnpj.Trim();
			cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
			if (cnpj.Length != 14)
			   return false;
			tempCnpj = cnpj.Substring(0, 12);
			soma = 0;
			for(int i=0; i<12; i++)
			   soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
			resto = soma % 11;
			if ( resto < 2)
			   resto = 0;
			else
			   resto = 11 - resto;
			digito = resto.ToString();
			tempCnpj += digito;
			soma = 0;
			for (int i = 0; i < 13; i++)
			   soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
			resto = soma % 11;
			if (resto < 2)
			    resto = 0;
			else
			   resto = 11 - resto;
			digito += resto.ToString();
			return cnpj.EndsWith(digito);
        }
    }
}