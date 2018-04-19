using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesJuego
{
    public class Partida
    {
        public List<Jugador> jugadores { get; set; }
        public Mazo mazo { get; set; }
        public string Turno { get; set; }
        public bool EstaCompleto { get; set; }

        public Partida() //Test
        {
            jugadores = new List<Jugador>();

        }

        public Partida Jugador(Jugador jugador) //Test
        {
            jugadores.Add(jugador);
            return this;
        }

        public Partida Mazo(Mazo mazoElegido) //Test
        {
            mazo = mazoElegido;
            return this;
        }

        public void RevisarCantidadJugadores() //Test
        {
            if (jugadores.Count() == 2)
            {
                this.EstaCompleto = true;
            }
            return;
        }


        public void MezclarCartas() //Test  (Esta publico porque sino el test no me lo tomaba)
        {
            var listaCartasAuxiliar = new List<Carta>();
            Random numeroNuevo = new Random();

            List<int> listaID = new List<int>();

            while (this.mazo.Cartas.Count > 0)
            {
                int n = numeroNuevo.Next(0, this.mazo.Cartas.Count); //PROBAR SI NO HAY QUE AGREGARLE EL +1
                listaCartasAuxiliar.Add(mazo.Cartas[n]);
                this.mazo.Cartas.RemoveAt(n);
            }

            mazo.Cartas = listaCartasAuxiliar;            
        
        }


        public void RepartirCartas() //Test
        {
            MezclarCartas(); //Mezclo el mazo asignado


            int Cont = 1;
            foreach (var item in this.mazo.Cartas)
            {
                //Asigna una carta a cada uno hasta que se terminen
                if ((Cont % 2) != 0)
                {
                    var participante1 = jugadores.First(x => x.NumeroJugador == NumJugador.uno);
                    participante1.Cartas.Add(item);
                }
                else
                {
                    var participante2 = jugadores.First(x => x.NumeroJugador == NumJugador.dos);
                    participante2.Cartas.Add(item);
                }
                Cont = Cont + 1;
            }
        }

        private void Deja5Cartas(Jugador jespecial, Jugador jafectado)    //Modifico la lista del afectado
        {
            var Lista5cartas = new List<Carta>();
            int c = 0;
            while (c != 5)
            {
                Lista5cartas[c] = jafectado.Cartas[c];
                c++;
            }

            jafectado.Cartas.RemoveRange(0, 5);

            foreach (var item in jespecial.Cartas)
            {
                jespecial.Cartas.Add(item); 
            }
            jafectado.Cartas = Lista5cartas; //Solo le quedan 5 cartas
        }
         
        private void ResolverCartasEspeciales(TipoDeCarta carta1, TipoDeCarta carta2, Jugador jugador1, Jugador jugador2)
        { //Doy por hecho que ambos se encuentran en la misma sala
            
            //Verde vs normal
            if ((carta1 == TipoDeCarta.Especial) && (carta2 == TipoDeCarta.Normal))
            {
                Deja5Cartas(jugador1,jugador2);
                return;
            }
            if ((carta2 == TipoDeCarta.Especial) && (carta1 == TipoDeCarta.Normal))
            {
                Deja5Cartas(jugador2,jugador1);
                return;
            }

            //Roja vs Amarilla 
            if ((TipoDeCarta.Roja == carta1) && (TipoDeCarta.Amarilla == carta2)) 
            {
                jugador1.Cartas.Add(jugador2.Cartas[0]); //Robo y asigno
                jugador2.Cartas.RemoveAt(0);
                return;
            }
            if ((TipoDeCarta.Amarilla == carta1) && (TipoDeCarta.Roja == carta2)) 
            {
                jugador2.Cartas.Add(jugador1.Cartas[0]);
                jugador1.Cartas.RemoveAt(0);
                return;
            }

            //Verde vs Roja
            if ((TipoDeCarta.Especial == carta1) && (TipoDeCarta.Roja == carta2)) 
            {
                Deja5Cartas(jugador1,jugador2);
                for (int i = 0; i < 1; i++) { jugador2.Cartas.Add(jugador1.Cartas[i]); }
                jugador1.Cartas.RemoveRange(0, 2);
                return;
            }
            if ((TipoDeCarta.Especial == carta2) && (TipoDeCarta.Roja == carta1))
            {
                Deja5Cartas(jugador2,jugador1);
                for (int i = 0; i < 1; i++) { jugador1.Cartas.Add(jugador2.Cartas[i]); }               
                jugador2.Cartas.RemoveRange(0, 2);
                return;
            }

            //Verde vs amarilla
            if ((TipoDeCarta.Especial == carta1) && (TipoDeCarta.Amarilla == carta2))
            {
                Deja5Cartas(jugador1,jugador2);
                jugador2.Cartas.Add(jugador1.Cartas[0]);
                jugador1.Cartas.RemoveAt(0);
                return;
            }
            if ((TipoDeCarta.Especial == carta2) && (TipoDeCarta.Amarilla == carta1))
            {
                Deja5Cartas(jugador2,jugador1);
                jugador1.Cartas.Add(jugador2.Cartas[0]);
                jugador2.Cartas.RemoveAt(0);
                return;
            }

            //Amarrilla vs Normal
            if ((TipoDeCarta.Amarilla == carta1) && (TipoDeCarta.Normal == carta2))
            {
                jugador1.Cartas.Add(jugador2.Cartas[0]);
                jugador2.Cartas.RemoveAt(0);
                return;
            }
            if ((TipoDeCarta.Amarilla == carta2) && (TipoDeCarta.Normal == carta1))
            {
                jugador2.Cartas.Add(jugador1.Cartas[0]);
                jugador1.Cartas.RemoveAt(0);
                return;
            }

            //Roja vs Normal
            if ((TipoDeCarta.Roja == carta1) && (TipoDeCarta.Normal == carta2))
            {
                for (int i = 0; i < 1; i++) { jugador1.Cartas.Add(jugador2.Cartas[i]); }              
                jugador2.Cartas.RemoveRange(0, 2);
                return;
            }
            if ((TipoDeCarta.Roja == carta2) && (TipoDeCarta.Normal == carta1))
            {
                for (int i = 0; i < 1; i++) { jugador2.Cartas.Add(jugador1.Cartas[i]); }               
                jugador1.Cartas.RemoveRange(0, 2);
                return;
            }
        } 

        public string ResolverCartasNormales (string atributo, int iDCartaJugador1, int iDCartaJugador2) //Test
        {
            string posible;
            
            var cartaJugador1 = jugadores[0].Cartas.Where(x => x.IdCarta == iDCartaJugador1).First().Atributos.Where(x => x.Nombre == atributo).FirstOrDefault();
            var cartaJugador2 = jugadores[1].Cartas.Where(x => x.IdCarta == iDCartaJugador2).First().Atributos.Where(x => x.Nombre == atributo).FirstOrDefault();

            //Se verifica que ambas cartas tengan la opcion elegida 
            if (cartaJugador1 == null || cartaJugador2 == null)
            {
                return posible ="ELEGIR OTRO ATRIBUTO"; //No es posible, debe elegir otro atributo.
            }
            else
            {
                

                if (cartaJugador1.Valor > cartaJugador2.Valor) //GANA JUGADOR 1
                {
                    int indice = jugadores[1].Cartas.IndexOf(jugadores[1].Cartas.Where(x => x.IdCarta == iDCartaJugador2).First());
                    jugadores[0].Cartas.Add(jugadores[1].Cartas.First(x=> x.IdCarta == iDCartaJugador2));
                    jugadores[1].Cartas.RemoveAt(indice);  //PREGUNTAR SI GANA ESA CARTA O UNA CUALQUIERA DEL MAZO DEL JUGADOR 2
                    return posible = "GANADOR JUGADOR 1";
                }
                else
                {   
                    if (cartaJugador2.Valor > cartaJugador1.Valor) //GANA JUGADOR 2
                    {
                        int indice = jugadores[0].Cartas.IndexOf(jugadores[0].Cartas.Where(x => x.IdCarta == iDCartaJugador1).First());
                        jugadores[1].Cartas.Add(jugadores[0].Cartas.First(x => x.IdCarta == iDCartaJugador1));
                        jugadores[0].Cartas.RemoveAt(indice);
                        return posible = "GANADOR JUGADOR 2"; 
                    }
                    else
                    {
                        return posible = "EMPATE";
                    }
                }

                             
            }
        }


    }


}

