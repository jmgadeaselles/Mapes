

//Declaracio de variables
var latlng;
var zoom;
var myOptions;
var map;
var objectesArray = [];
var textBuscarJS;


function UrlBase() {
    if (location.host.indexOf('localhost') >= 0) {
        return 'http://localhost/m/';
    } else {
        return location.protocol + '//' + location.host + '/m/';
    }
}



//Mapa basic
function MontarMapa(lat_map, lng_map, zoom_map, geolocation) {
    latlng = new google.maps.LatLng(lat_map, lng_map);
    // Try HTML5 geolocation
    if (geolocation == true) {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (position) {
                latlng = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
                map.setCenter(latlng);
                map.setZoom(17);
            }, function () {
                //No fem res, ja tenim el zoom adequat
                //map.setZoom(2);
            });
        }
    }
    //Montem el mapa
    myOptions = {
        zoom: zoom_map,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
}



function Refrescar(map, objectesArray) {
    //map.clearOverlays();
    latlng = map.getCenter();
    zoom = map.getZoom();
    getGeoRSS(map, objectesArray, textBuscarJS);
}



function NavegarMapa(textBuscar) {
    google.maps.Map.prototype.clearOverlays = function () {
        if (objectesArray) {
            for (var i = 0; i < objectesArray.length; i++) {
                objectesArray[i].setMap(null);
            }
        }
        objectesArray.length = 0;
    }
    textBuscarJS = textBuscar;
    //Carga del GeoRSS
    getGeoRSS(map, objectesArray, textBuscar);

    //Eventos
    google.maps.event.addListener(map, 'idle', function () { Refrescar(map, objectesArray) });
}




function getGeoRSS(map, objectesArray, textBuscar) {
    var Llistat = '';
    //Obtenim les foreres del mapa
    var Limites = map.getBounds();
    if (Limites != null) {
        var NE = Limites.getNorthEast();
        var SW = Limites.getSouthWest();
        var N = NE.lat();
        var E = NE.lng();
        var S = SW.lat();
        var O = SW.lng();
    }
    var Z = map.getZoom();


    if (N != undefined) {

        $.ajax({
            type: "GET",
            url: UrlBase() + "../rss/Dades.aspx?N=" + N + "&E=" + E + "&S=" + S + "&O=" + O + "&Z=" + Z + "&T=" + textBuscar,
            dataType: "xml",
            success: function (xml) {
                map.clearOverlays();
                $(xml).find('item').each(function () {
                    //Arrepleguem dades
                    var id = $(this).find('guid').text();
                    var titulo = $(this).find('title').text();
                    var descripcion = $(this).find('description').text();
                    var link = $(this).find('link').text();
                    var lat = $(this).find('lat').text();
                    var lng = $(this).find('long').text();
                    var icono = $(this).find('icon').text();
                    var foto = $(this).find('img').text();
                    //Definim el punt 
                    var myLatlng = new google.maps.LatLng(lat, lng);
                    var marker = new google.maps.Marker({
                        position: myLatlng,
                        title: titulo,
                        icon: icono
                    });
                    //event
                    var img_foto = '';
                    if (foto != '') {
                        img_foto = '<img src="' + foto + '" />';
                    }
                    var text_info = descripcion + '<br /><a href="javascript:detall(\'' + id + '\')">Info</a>';
                    var infoglobo = '<div id="content">'
                                    + '<h3>' + titulo + '</h3>'
                                    + '<div id="bodyContent">'

                                    + '<table>'
                                    + '<tr>'
                                    + '<td>' + img_foto + '</td>'
                                    + '<td style="vertical-align:top;">' + text_info + '</td>'
                                    + '</tr>'
                                    + '</table>'

                                    + '</div>'
                                    + '</div>';
                    var infowindow = new google.maps.InfoWindow({
                        content: infoglobo
                    });
                    google.maps.event.addListener(marker, 'click', function () {
                        infowindow.open(map, marker);
                    });
                    //Insertem el punt
                    objectesArray.push(marker);
                    marker.setMap(map);

                    Llistat = Llistat + '<tr class="fons_item">'
                                      + '<td class="celda_img"><img src="' + icono + '"/></td>'
                                      + '<td><a href="javascript:detall(\'' + id + '\')" class="titol_lloc">' + titulo + '</a>'
                                      + '<br/>' + descripcion + '</td>'
                                      + '</tr>'
                                      + '<tr>'
                                      + '<td>'
                                      + '<div id="detall_' + id + '" class="capa_detalls"></div>'
                                      + '</td>'
                                      + '</tr>';
                });
                $("#LlistatLlocs").html('<table cellspacing="0" class="Llistat">' + Llistat + '</table>');



            }
        });
    }
}


function detall(id) {
    var html = '<div style="height:auto;top:0px;left:0px;right:0px;border:0px;text-align:center;vertical-align:middle;"><div style="position: absolute;top:50%;left:50%;right:50%;bottom:50%;vertical-align:middle">Loading...</div></div>';
    document.getElementById('contingut').style.visibility = 'visible';
    document.getElementById('contingut').style.display = 'inline';
    document.getElementById("contingut").contentDocument.body.innerHTML = html;
    document.getElementById("fons_detall").style.visibility = 'visible';
    document.getElementById("contingut_detall").style.visibility = 'visible';
    document.getElementById("contingut").src = UrlBase() + 'items/Lloc.aspx?id=' + id;
}


function detall_nou() {
    var html = '<div style="height:auto;top:0px;left:0px;right:0px;border:0px;text-align:center;vertical-align:middle;"><div style="position: absolute;top:50%;left:50%;right:50%;bottom:50%;vertical-align:middle">Loading...</div></div>';
    document.getElementById('contingut').style.visibility = 'visible';
    document.getElementById('contingut').style.display = 'inline';
    document.getElementById("contingut").contentDocument.body.innerHTML = html;
    document.getElementById("fons_detall").style.visibility = 'visible';
    document.getElementById("contingut_detall").style.visibility = 'visible';
    document.getElementById("contingut").src = UrlBase() + 'items/LlocEdt.aspx?lat=' + latlng.lat() + '&lng=' + latlng.lng() + '&zoom=' + zoom;
}




function tancar_detall() {
    window.parent.document.getElementById("fons_detall").style.visibility = 'hidden';
    window.parent.document.getElementById("contingut_detall").style.visibility = 'hidden';
    //window.parent.document.getElementById('contingut').style.visibility = 'hidden';
    window.parent.document.getElementById('contingut').style.display = 'none';
}





