using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace ReferenceConsoleRedisApp
{
    class Program
    {
        static readonly ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("redis-17704.c299.asia-northeast1-1.gce.redns.redis-cloud.com:17704,password=MeknVcz3slTYjhscGzGNv82LwoBbb3mk");

        static async Task Main(string[] args)
        {
            var db = redis.GetDatabase();

            while (true)
            {
                Console.WriteLine("Selecione uma opção:");
                Console.WriteLine("1. Criar cadastro");
                Console.WriteLine("2. Atualizar cadastro");
                Console.WriteLine("3. Excluir cadastro");
                Console.WriteLine("4. Listar cadastros");
                Console.WriteLine("5. Sair");

                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        // Criar cadastro
                        await CriarCadastro(db);
                        break;
                    case "2":
                        // Atualizar cadastro
                        await AtualizarCadastro(db);
                        break;
                    case "3":
                        // Excluir cadastro
                        await ExcluirCadastro(db);
                        break;
                    case "4":
                        // Listar cadastros
                        await ListarCadastros(db);
                        break;
                    case "5":
                        // Sair do programa
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        static async Task CriarCadastro(IDatabase db)
        {
            Console.WriteLine("Digite a chave do cadastro:");
            var chave = Console.ReadLine();

            Console.WriteLine("Digite os detalhes do cadastro:");
            var detalhes = Console.ReadLine();

            await db.StringSetAsync(chave, detalhes);
            Console.WriteLine("Cadastro criado com sucesso!");
        }

        static async Task AtualizarCadastro(IDatabase db)
        {
            Console.WriteLine("Digite a chave do cadastro que deseja atualizar:");
            var chave = Console.ReadLine();

            Console.WriteLine("Digite os novos detalhes do cadastro:");
            var novosDetalhes = Console.ReadLine();

            await db.StringSetAsync(chave, novosDetalhes);
            Console.WriteLine("Cadastro atualizado com sucesso!");
        }

        static async Task ExcluirCadastro(IDatabase db)
        {
            Console.WriteLine("Digite a chave do cadastro que deseja excluir:");
            var chave = Console.ReadLine();

            await db.KeyDeleteAsync(chave);
            Console.WriteLine("Cadastro excluído com sucesso!");
        }

        static async Task ListarCadastros(IDatabase db)
        {
            Console.WriteLine("Listando cadastros:");

            var keys = await db.ExecuteAsync("KEYS", "*");
            foreach (var key in (RedisResult[])keys)
            {
                var detalhes = await db.StringGetAsync((string)key);
                Console.WriteLine($"Chave: {key}, Valor: {detalhes}\n");
            }
        }
    }
}
