namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();
        private List<string> vagas = new List<string>();
        Dictionary<string, string> veiculoVagaMap = new Dictionary<string, string>(); // Adiciono um Dictionary para associar o numero da placa com a vaga que esta estacionado

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            string numeroPlaca = string.Empty;
            string numeroVaga = string.Empty;
            Console.Clear();

            Console.WriteLine("Digite a placa do veículo para estacionar:");
            numeroPlaca = Console.ReadLine();

            if (!string.IsNullOrEmpty(numeroPlaca)) // Verifica se o numeroPlaca não é vazio ou invalido
            {
                while (veiculos.Contains(numeroPlaca)) // Verifica se a Placa ja existe na lista, se existir, pedirá para adicionar outra Placa valido
                {
                    Console.WriteLine("O Veiculo já esta estacionado. Digite outro veículo");
                    numeroPlaca = Console.ReadLine();
                }

                veiculos.Add(numeroPlaca);
                Console.WriteLine($"O veículo {numeroPlaca} foi adicionado ao estacionamento.{Environment.NewLine}");
            }
            else
            {
                Console.WriteLine("Veículo Não foi adicionado ao estacionamento.");
            }

            Console.WriteLine("Digite a vaga que estará estacionado:");
            numeroVaga = Console.ReadLine();

            if (!string.IsNullOrEmpty(numeroVaga)) // Verifica se o numeroVaga não é vazio ou invalido
            {
                while (vagas.Contains(numeroVaga)) // Verifica se a Vaga ja existe na lista, se existir, pedirá para adicionar outra vaga valida
                {
                    Console.WriteLine("A vaga já está ocupada! Digite outra vaga.");
                    numeroVaga = Console.ReadLine();
                }

                vagas.Add(numeroVaga);
                veiculoVagaMap[numeroPlaca] = numeroVaga;
                Console.WriteLine($"A vaga {numeroVaga} foi alocada ao veículo {numeroPlaca}.{Environment.NewLine}");
            }
            else
            {
                Console.WriteLine("A vaga não está disponível.");
            }

        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");

            string placa = string.Empty;
            Console.Clear();

            Console.WriteLine("Digite a placa do veículo para ser removido");
            placa = Console.ReadLine();

            if (veiculos.Any(x => x.ToUpper() == placa.ToUpper())) // Verifica se o veículo existe
            {
                int horas = 0;
                decimal valorTotal = 0;
                int tipoPagamento = 0;

                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");
                horas = Convert.ToInt32(Console.ReadLine());

                if (horas > 0)
                {
                    valorTotal = precoInicial + precoPorHora * horas;
                    Console.WriteLine($"O preço total do veículo {placa} estacionado por {horas} horas é de: R$ {valorTotal}");
                }

                Console.WriteLine($"Deseja pagar o valor de R${valorTotal} em dineiro ou cartão?");
                Console.WriteLine("1 - Dinheiro");
                Console.WriteLine("2 - Cartão");
                tipoPagamento = Convert.ToInt32(Console.ReadLine());

                switch (tipoPagamento)
                {
                    case 1:
                        Console.WriteLine("Pagamento efetuado em dinheiro.");
                        break;
                    case 2:
                        Console.WriteLine("Pagamento efetuado com cartão.");
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                if (tipoPagamento == 1 || tipoPagamento == 2)
                {
                    veiculos.Remove(placa);
                    if (veiculoVagaMap.ContainsKey(placa))
                    {
                        string vagaAssociada = veiculoVagaMap[placa];
                        vagas.Remove(vagaAssociada); // Remove a vaga associada ao veículo
                        veiculoVagaMap.Remove(placa); // Remove a associação do veículo e vaga
                        Console.WriteLine($"O veículo {placa} estacionado na vaga {vagaAssociada} foi removido, e o preço total foi de: R$ {valorTotal}");
                    }
                }

            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");

                int contador = 1;
                foreach (var veiculo in veiculos)
                {
                    if (veiculoVagaMap.ContainsKey(veiculo))
                    {
                        string vagaAssociada = veiculoVagaMap[veiculo];
                        Console.WriteLine($"{contador} - {veiculo} estacionado na vaga {vagaAssociada}");
                        contador++;
                    }
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
    }
}
