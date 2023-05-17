using System;
using System.Collections.Generic;

namespace PrimeFactorization
{
    class Factorizar
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese un número entero para factorizar:");
            int number = Convert.ToInt32(Console.ReadLine());

            List<int> primeFactors = Factorize(number);

            Console.WriteLine($"Los factores primos del número {number} son:");
            foreach (int factor in primeFactors)
            {
                Console.WriteLine(factor);
            }
        }

        static List<int> Factorize(int number)
        {
            List<int> primeFactors = new List<int>();

            // Encontrar los factores primos utilizando la criba de Eratóstenes
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                while (number % i == 0)
                {
                    primeFactors.Add(i);
                    number /= i;
                }
            }

            // Si el número no es divisible por ningún factor menor o igual que su raíz cuadrada,
            // entonces el número en sí mismo es un factor primo
            if (number > 1)
            {
                primeFactors.Add(number);
            }

            return primeFactors;
        }
    }
}
