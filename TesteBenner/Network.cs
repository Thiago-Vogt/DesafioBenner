using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteBenner
{
    public class Network
    {
        private readonly int tamanho;
        private readonly List<HashSet<int>> conexoes;

        public Network(int tamanho)
        {
            if (tamanho < 0) {
                throw new ArgumentException("Valor inválido, deve ser um número inteiro maior que 0");
            }
            this.tamanho = tamanho;
            conexoes = new List<HashSet<int>>(tamanho + 1);
            for (int i = 0; i <= tamanho; i++)
            {
                conexoes.Add(new HashSet<int>());
            }
        }

        public void connect(int a , int b)
        {
            validarElemento(a);
            validarElemento(b);

            if(a == b)
            {
                throw new ArgumentException("Um número não pode se conectar com ele mesmo");
            }

            conexoes[a].Add(b);
            conexoes[b].Add(a);
        }

        public void disconnect(int a, int b)
        {
            validarElemento(a);
            validarElemento(b);

            if (a == b)
            {
                throw new ArgumentException("Um número não pode se desconectar dele mesmo");
            }

            if(!conexoes[a].Contains(b))
            {
                throw new Exception("Os números não estão conectados");
            }

            conexoes[a].Remove(b);
            conexoes[b].Remove(a);

        }

        public Boolean query(int a, int b)
        {
            validarElemento(a);
            validarElemento(b);

            return levelConnection(a, b) > 0;
        }

        public int levelConnection(int a, int b)
        {
            validarElemento(a);
            validarElemento(b);

            if (conexoes[a].Contains(b)) return 1; //conexão direta

            //Busca em largura para conexão indireta
            var fila = new Queue<(int, int)>();
            var visitado = new HashSet<int> { a };
            fila.Enqueue((a, 1));

            while (fila.Count > 0)
            {
                var (atual, level) = fila.Dequeue();

                foreach (var vizinho in conexoes[atual])
                {
                    if (vizinho == b) return level;

                    if (visitado.Add(vizinho))
                    {
                        fila.Enqueue((vizinho, level + 1));
                    }
                }
            }

            return 0; // Não há conexão entre os elementos
        }

        //valida se elemento está dentro tamanho setado
        public void validarElemento(int num)
        {
            if (num < 0 || num > tamanho) {
                throw new ArgumentOutOfRangeException("Elemento fora do alcance");
            }
        }
    }
}
