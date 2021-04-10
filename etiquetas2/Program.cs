using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace etiquetas2
{

	public class Paciente
	{ 
		public string Nome { get; set; }
		public string NomeSocial { get; set; }
		public string CPF { get; set; }
		public int Prontuario { get; set; }
		public DateTime DataNascimento { get; set; }
		public int Idade { get; set; }
		public string NomeMae { get; set; }


		public Paciente()
		{
			this.Nome = string.Empty;
			this.NomeSocial = string.Empty;
			this.CPF = string.Empty;
			this.Prontuario = 0000;
			this.DataNascimento = new DateTime();
			this.Idade = 000;
			this.NomeMae = string.Empty;
		}
	}

	static class Program
	{
		/// <summary>
		/// Ponto de entrada principal para o aplicativo.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Paciente paciente = new Paciente();
			if (args.Any())
			{
				var file = args[0];
				if (File.Exists(file))
				{
					var fileValues = File.ReadAllText(file);
					var matchNomePaciente = Regex.Match(fileValues, @"nome do Paciente:\s(.+); nome social", RegexOptions.IgnoreCase);
					var matchNomeSocial = Regex.Match(fileValues, @"nome social:\s(.+); cpf", RegexOptions.IgnoreCase);
					var matchCPF = Regex.Match(fileValues, @"cpf: (.*); nro_pront", RegexOptions.IgnoreCase);
					var matchNumeroPront = Regex.Match(fileValues, @"nro_pront: (\d*); data_nascimento", RegexOptions.IgnoreCase);
					var matchDataNasc = Regex.Match(fileValues, @"data_nascimento: (\d{1,2})\/(\d{1,2})\/(\d{2,4}); idade", RegexOptions.IgnoreCase);
					var matchIdade = Regex.Match(fileValues, @"idade: (\d+); nome_mae", RegexOptions.IgnoreCase);
					var matchNomeMae = Regex.Match(fileValues, @"nome_mae:\s(.+)", RegexOptions.IgnoreCase);

					if (matchNomePaciente.Success)
						paciente.Nome = matchNomePaciente.Groups[1].Value;
					
					if (matchNomeSocial.Success)
						paciente.NomeSocial = matchNomeSocial.Groups[1].Value;

					if (matchCPF.Success)
						paciente.CPF = matchCPF.Groups[1].Value;

					if (matchNumeroPront.Success)
						paciente.Prontuario = int.Parse(matchNumeroPront.Groups[1].Value);

					if (matchDataNasc.Success)
						paciente.DataNascimento = new DateTime(int.Parse(matchDataNasc.Groups[3].Value), int.Parse(matchDataNasc.Groups[2].Value), int.Parse(matchDataNasc.Groups[1].Value));

					if (matchIdade.Success)
						paciente.Idade = int.Parse(matchIdade.Groups[1].Value);

					if (matchNomeMae.Success)
						paciente.NomeMae = matchNomeMae.Groups[1].Value;
				}
			}
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new TSCPrint(paciente));
		}
	}
}
