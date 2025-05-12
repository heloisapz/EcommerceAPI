using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

//classe base para notificações de validação
namespace Entities.Notifications
{
    public class Notifies
    {
        public Notifies()
        {
            //inicializa a lista de notificações
            Notitycoes = new List<Notifies>();
        }

        [NotMapped] //esta propriedade não será salva no banco de dados
        public string NomePropriedade { get; set; }

        [NotMapped]
        public string mensagem { get; set; }

        [NotMapped]
        public List<Notifies> Notitycoes; //lista de todos os erros encontrados

        //valida se uma string está preenchida
        public bool ValidarPropriedadeString(string valor, string nomePropriedade)
        {
            if (string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                //adiciona notificação de erro caso valor seja nulo ou vazio
                Notitycoes.Add(new Notifies
                {
                    mensagem = "Campo Obrigatório",
                    NomePropriedade = nomePropriedade
                });

                return false; //indica que a validação falhou
            }

            return true; //a string é válida
        }

        //valida se um valor inteiro é maior que zero
        public bool ValidarPropriedadeInt(int valor, string nomePropriedade)
        {
            if (valor < 1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                //adiciona notificação de erro se número for menor que 1
                Notitycoes.Add(new Notifies
                {
                    mensagem = "Valor deve ser maior que 0",
                    NomePropriedade = nomePropriedade
                });

                return false; //validação falhou
            }

            return true; //o inteiro é válido
        }

        //valida se um valor decimal é maior que zero
        public bool ValidarPropriedadeDecimal(decimal valor, string nomePropriedade)
        {
            if (valor < 1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                //adiciona notificação se o decimal for inválido
                Notitycoes.Add(new Notifies
                {
                    mensagem = "Valor deve ser maior que 0",
                    NomePropriedade = nomePropriedade
                });

                return false; //decimal inválido
            }

            return true; //o decimal é válido
        }
    }
}
