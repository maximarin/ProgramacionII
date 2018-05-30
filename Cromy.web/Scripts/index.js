var esJugador1 = {};

$(function () {
    // Reference the auto-generated proxy for the hub.  
    var juego = $.connection.juegoHub;

    //#region Funciones para la gestión de partidas.

    juego.client.agregarPartida = function (partida) {
        addGameItem(partida);
    };

    juego.client.agregarPartidas = function (partidas) {
        $.each(partidas, function (index, value) {
            addGameItem(value);
        });
    };

    juego.client.agregarMazos = function (mazos) {
        $.each(mazos, function (index, value) {
            $('#card-type').append($('<option/>', {
                value: value,
                text: value
            }));
        });
    };

    juego.client.esperarJugador = function () {
        $.blockUI({
            message: '<h5>Esperando a que otro jugador se una a la partida...</h5>'
        });
    };
    //////////////////////////////////////////////////////////////////////////////////////
    juego.client.partidaDuplicada = function () {
        alert('Ya existe una partida con el mismo nombre.');
    };
    //////////////////////////////////////////////////////////////////////////////////////


    juego.client.eliminarPartida = function (partida) {
        $('[data-game-id="' + partida + '"]').parent().parent().remove();
    };

    //#endregion

    //#region Funciones para el juego.

    juego.client.ganar = function () {
        ganarJuego();
    };

    juego.client.perder = function () {
        perderJuego();
    };

    juego.client.ganarMano = function () {
        ganarMano();
    };

    juego.client.perderMano = function () {
        perderMano();
        deshabilitarJuego();
    };

    juego.client.dibujarTablero = function (jugador1, jugador2, mazo) {
        // Reconocer si es jugador 1 o 2.
        var cardType = mazo.Nombre;
        var attributes = mazo.NombreAtributos;

        var player1, player2 = {};

        if (esJugador1) {
            player1 = jugador1;
            player2 = jugador2;
        }
        else {
            player2 = jugador1;
            player1 = jugador2;
        }

        $.each(player1.Cartas, function (index, value) {
            $('#baraja-el').append(getNewCard(value, cardType));
        });

        $.each(player2.Cartas, function (index, value) {
            $('#baraja-2').append(getNewCardForPlayer2(value, cardType));
        });

        $.each(attributes, function (index, value) {
            $('.button-area').append(getNewAttribute(value));
        });

        baraja = $('#baraja-el').baraja();
        baraja2 = $('#baraja-2').baraja();

        baraja.$playerName.text(player1.Nombre);
        baraja2.$playerName.text(player2.Nombre);

        if (esJugador1) {
            habilitarJuego();
        }
        else {
            deshabilitarJuego();
        }

        backCard = getBackCard(cardType);

        $('#start-session-options').hide();
        $('#game-area').show();

        $.unblockUI();
    };

    //#endregion

    // Start the connection.
    $.connection.hub.start().done(function () {
        $('#start-game').click(function () {
            var cardType = $('#card-type').val();
            var gameName = $('#game-name').val();

            if (!cardType || !gameName) {
                alert('Debe seleccionar un mazo e indicar el nombre de la partida...');
            }
            else {
                juego.server.crearPartida($('#user-name').val(), $('#game-name').val(), $('#card-type').val());
                esJugador1 = true;
            }
        });

        $('body').on('click', '.join-game', function () {
            var gameId = $(this).attr('data-game-id');

            juego.server.unirsePartida($('#user-name').val(), gameId);
            esJugador1 = false;
        });

        $('body').on('click', '.button-area button', function () {

            deshabilitarJuego();

            var attributeId = $(this).attr('data-attribute-id');
            var cardCode = $(baraja.getFirstCard()).find('img').attr('data-card-code');

            juego.server.cantar(attributeId, cardCode);
        });

        juego.server.obtenerPartidas();
        juego.server.obtenerMazos();
    });

    $('#start-session').click(function () {
        var userName = $('#user-name').val();

        if (!userName) {
            alert('Debe ingresar el nombre de jugador...');
        }
        else {
            $('#user-session-options').fadeOut('slow');
            $('#start-session-options').fadeIn('slow');
        }
    });
});

function addGameItem(partida) {
    var element = '<tr>';
    element += '<td>' + partida.Usuario + '</td>';
    element += '<td>' + partida.Nombre + '</td>';
    element += '<td>' + partida.Mazo + '</td>';
    element += '<td><button type="button" class="btn btn-primary btn-xs join-game" data-game-id="' + partida.Nombre + '">UNIRSE</button></td>';
    element += '</tr>';

    $('#available-games').append(element);
};

function getNewCard(card, cardType) {
    var newCard = '<li><img class="front-card" src="/Mazos/';
    newCard += cardType;
    newCard += '/carta_' + card.Codigo + '.jpg';
    newCard += '" alt="' + card.Nombre + '" data-card-code="' + card.Codigo + '" /></li>';

    return newCard;
};

function getNewCardForPlayer2(card, cardType) {
    var newCard = '<li class="card-container">'
    newCard += getBackCard(cardType);
    newCard += '<img class="front-card" src="/Mazos/';
    newCard += cardType;
    newCard += '/carta_' + card.Codigo + '.jpg';
    newCard += '" alt="' + card.Nombre + '" /></li>';

    return newCard;
};

function getBackCard(cardType) {
    var newCard = '<img class="back-card" src="/Mazos/';
    newCard += cardType;
    newCard += '/carta_reversa.jpg';
    newCard += '" alt="Carta Reversa" />';

    return newCard;
}

function getNewAttribute(attribute) {
    var newAttribute = '<button type="button" class="btn btn-default attributeStyle" data-attribute-id="';
    newAttribute += attribute;
    newAttribute += '" role="button">';
    newAttribute += attribute;
    newAttribute += '</button>';
    return newAttribute;
};