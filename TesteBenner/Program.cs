namespace TesteBenner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Network teste = new Network(8);
            teste.connect(1, 2);
            teste.connect(2,3);
            teste.connect(3,4);
            Console.WriteLine(teste.levelConnection(1,4));
            Console.WriteLine(teste.query(1,5));
            //teste.disconnect(1, 2);
            //Console.WriteLine(teste.levelConnection(1, 2));
        }
    }
}
