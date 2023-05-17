using System;
using System.Collections.Generic;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("¡Bienvenido al juego de Blackjack!");

            // Crear una nueva baraja y mezclarla
            Deck deck = new Deck();
            deck.Shuffle();

            // Repartir las cartas iniciales
            List<Card> manoJugador = new List<Card>();
            List<Card> manoCrupier = new List<Card>();

            manoJugador.Add(deck.DrawCard());
            manoCrupier.Add(deck.DrawCard());
            manoJugador.Add(deck.DrawCard());
            manoCrupier.Add(deck.DrawCard());

            // Mostrar las cartas del jugador y la carta visible del crupier
            Console.WriteLine("Tu mano: " + string.Join(", ", manoJugador));
            Console.WriteLine("Carta visible del crupier: " + manoCrupier[0]);

            // Jugar turno del jugador
            while (true)
            {
                Console.WriteLine("¿Qué deseas hacer? (P: Pedir carta, M: Mantener)");
                string eleccion = Console.ReadLine().ToUpper();

                if (eleccion == "P")
                {
                    manoJugador.Add(deck.DrawCard());
                    Console.WriteLine("Tu mano: " + string.Join(", ", manoJugador));

                    if (ValorMano(manoJugador) > 21)
                    {
                        Console.WriteLine("Te has pasado de 21. ¡Has perdido!");
                        return;
                    }
                }
                else if (eleccion == "M")
                {
                    break;
                }
            }

            // Jugar turno del crupier
            while (ValorMano(manoCrupier) < 17)
            {
                manoCrupier.Add(deck.DrawCard());
            }

            Console.WriteLine("Mano del crupier: " + string.Join(", ", manoCrupier));

            // Determinar el resultado
            int puntuacionJugador = ValorMano(manoJugador);
            int puntuacionCrupier = ValorMano(manoCrupier);

            Console.WriteLine("Tu puntuación: " + puntuacionJugador);
            Console.WriteLine("Puntuación del crupier: " + puntuacionCrupier);

            if (puntuacionJugador > puntuacionCrupier && puntuacionJugador <= 21 || puntuacionCrupier > 21)
            {
                Console.WriteLine("¡Has ganado!");
            }
            else if (puntuacionJugador == puntuacionCrupier)
            {
                Console.WriteLine("Empate.");
            }
            else
            {
                Console.WriteLine("¡Has perdido!");
            }
        }

        static int ValorMano(List<Card> mano)
        {
            int valor = 0;
            int cantidadAses = 0;

            foreach (Card carta in mano)
            {
                if (carta.Rango == Rango.As)
                {
                    valor += 11;
                    cantidadAses++;
                }
                else if (carta.Rango == Rango.Jota || carta.Rango == Rango.Reina || carta.Rango == Rango.Rey)
                {
                    valor += 10;
                }
                else
                {
                    valor += (int)carta.Rango;
                }
            }

            while (valor > 21 && cantidadAses > 0)
            {
                valor -= 10;
                cantidadAses--;
            }

            return valor;
        }
    }

    // Enumeración para los rangos de las cartas
    enum Rango
    {
        As = 1, Dos, Tres, Cuatro, Cinco, Seis, Siete, Ocho, Nueve, Diez, Jota, Reina, Rey
    }
    // Enumeración para los palos de las cartas
    enum Palo
    {
        Tréboles, Diamantes, Corazones, Picas
    }

    // Clase que representa una carta
    class Card
    {
        public Rango Rango { get; }
        public Palo Palo { get; }

        public Card(Rango rango, Palo palo)
        {
            Rango = rango;
            Palo = palo;
        }

        public override string ToString()
        {
            return $"{Rango} de {Palo}";
        }
    }

    // Clase que representa una baraja de cartas
    class Deck
    {
        private List<Card> cartas;
        private Random aleatorio;

        public Deck()
        {
            cartas = new List<Card>();
            aleatorio = new Random();

            foreach (Palo palo in Enum.GetValues(typeof(Palo)))
            {
                foreach (Rango rango in Enum.GetValues(typeof(Rango)))
                {
                    cartas.Add(new Card(rango, palo));
                }
            }
        }

        public void Shuffle()
        {
            int n = cartas.Count;
            while (n > 1)
            {
                n--;
                int k = aleatorio.Next(n + 1);
                Card carta = cartas[k];
                cartas[k] = cartas[n];
                cartas[n] = carta;
            }
        }

        public Card DrawCard()
        {
            Card carta = cartas[0];
            cartas.RemoveAt(0);
            return carta;
        }
    }
}