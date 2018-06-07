﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesJuego
{
    public class Juego
    {
        public List<Partida> Partidas { get; set; }
        public List<Ranking> Rankings { get; set; }
        public List<Jugador> Jugadores { get; set; }
        public List<Mazo> Mazos { get; set; }

        //HAY UN SOLO JUEGO, POR LO TANTO SE APLICA SINGLETON 



        public Juego()
        {
            this.Partidas = new List<Partida>();
            this.Mazos = new List<Mazo>();
            this.Mazos = AgregarMazos();
        }

        public Partida AgregarPartida(Partida nuevaPartida)
        {
            Partidas.Add(nuevaPartida);
            return nuevaPartida;
        }  

        public List<Mazo> AgregarMazos()
        {
            var lines = File.ReadAllLines(@"C:\Users\Juan Aira\Desktop\P\Cromy.web\Mazos\X-Men\Informacion.txt");
            int cont = 0;
            var nuevoMazo = new Mazo();
            string[] datos;
            List<Atributo> Atributos = new List<Atributo>();

            foreach (var line in lines)   //LEO EL ARCHIVO
            {
                if (cont == 0)
                {
                    nuevoMazo.Nombre = line;  //SI ESTOY EN LA PRIMER LÍNEA DEFINO EL NOMBRE DEL MAZO 
                }
                else
                {
                    datos = line.Split('|');

                    if (cont == 1)     //SI ES LA SEGUNDA LÍNEA AÑADO LOS ATRIBUTOS A UN VECTOR 
                    {

                        for (int i = 0; i < datos.Length; i++)
                        {
                            var atrib = new Atributo();
                            atrib.Nombre = datos[i];
                            Atributos.Add(atrib);
                        }
                    }
                    else
                    {
                        var nuevaCarta = new Carta();        //A PARTIR DE LA TERCER LÍNEA VOY CREANDO LAS CARTAS Y ASIGNANDO LOS VALORES A LOS ATRIBUTOS
                        nuevaCarta.Atributos = Atributos;
                        nuevaCarta.TipoCarta = TipoDeCarta.Normal;
                        int j = 0;
                        for (int i = 0; i < datos.Length; i++)
                        {
                            if (i == 0)
                            {
                                nuevaCarta.IdCarta = Convert.ToInt32(datos[i]);
                            }
                            else
                            {
                                if (i == 1)
                                {
                                    nuevaCarta.Nombre = datos[i];

                                }
                                else
                                {
                                    nuevaCarta.Atributos[j].Valor = Convert.ToDouble(datos[i]);
                                    j++;
                                }
                                    


                            }

                           

                        }

                        nuevoMazo.Cartas.Add(nuevaCarta);

                    }


                }
                cont++;
            }

            var cartaAmarilla = new Carta();
            cartaAmarilla.IdCarta = 1;
            cartaAmarilla.TipoCarta = TipoDeCarta.Amarilla;
            cartaAmarilla.Atributos = null;

            var cartaRoja = new Carta();
            cartaRoja.IdCarta = 2;
            cartaRoja.TipoCarta = TipoDeCarta.Roja;
            cartaRoja.Atributos = null;


            var cartaEspecial = new Carta();
            cartaEspecial.IdCarta = 3;
            cartaEspecial.TipoCarta = TipoDeCarta.Especial;
            cartaEspecial.Atributos = null;

            nuevoMazo.Cartas.Add(cartaAmarilla); nuevoMazo.Cartas.Add(cartaRoja); nuevoMazo.Cartas.Add(cartaEspecial);


            Mazos.Add(nuevoMazo);

            return this.Mazos;
        }

        public List<PartidasHub> RetornarPartidas()
        {
            List<PartidasHub> ListaRetornar = new List<PartidasHub>();

            foreach (var item in this.Partidas)
            {
                var x = new PartidasHub();
                x.Usuario = item.jugadores[0].nombre;
                x.Mazo = item.mazo.Nombre;
                x.Nombre = item.nombre;

                ListaRetornar.Add(x);
            }
            return ListaRetornar;
        }

        public Mazo BuscarMazo(string nombre)
        {
            var mazo = Mazos.Where(x => x.Nombre == nombre).FirstOrDefault();
            return mazo;
        }

        public Partida AgregarSegundoJugador(Jugador jug, string parti)
        {
            var par = new Partida();
            foreach (var item in this.Partidas)
            {
                 if (item.nombre==parti)
                {
                    item.jugadores.Add(jug);
                    par = item;
                }
            }
            return par;
        }

        public List<string> NombreDeLosMazos()
        {
            var ListaRertornar = new List<string>();

            foreach (var item in this.Mazos)
            {
                ListaRertornar.Add(item.Nombre);
            }

            return ListaRertornar;
        }
    }
}
