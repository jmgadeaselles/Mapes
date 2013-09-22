


function MostrarPujarFoto(id,i) {
    document.getElementById('FonsPujarFoto').style.visibility = 'visible';
    document.getElementById('PanelPujarFoto').style.visibility = 'visible';
    $('#PanelPujarFoto').html('Loading...');
    $('#PanelPujarFoto').load(UrlBase() + 'items/img/PujarFoto.aspx?id=' + id + '&i=' + i);
    return false;
}


function TancarPujarFoto() {
    document.getElementById('FonsPujarFoto').style.visibility = 'hidden';
    document.getElementById('PanelPujarFoto').style.visibility = 'hidden';
    document.getElementById('status').style.visibility = 'hidden';
    document.getElementById('loading').style.visibility = 'hidden';
    return false;
}




function mostrarFoto(id,idioma,foto) {        
    window.parent.document.getElementById('fons_foto').style.visibility = 'visible';
    window.parent.document.getElementById('contingut_foto').style.visibility = 'visible';
    $(window.parent.document.getElementById('contingut_foto')).html('Loading...');
    $(window.parent.document.getElementById('contingut_foto')).load(UrlBase() + 'items/foto/Foto.aspx?id=' + id + '&i=' + idioma);    
    return false;
}

function TancarFoto() {
    alert('hola des de .js');
    window.parent.document.getElementById('fons_foto').style.visibility = 'hidden';
    window.parent.document.getElementById('contingut_foto').style.visibility = 'hidden';
}



function MostrarHistorial(id) {
    //document.getElementById('FonsPujarFoto').style.visibility = 'visible';
    document.getElementById('PanelHistorial').style.visibility = 'visible';
    $('#PanelHistorial').html('Loading...');
    $('#PanelHistorial').load(UrlBase() + 'items/Historial.aspx?id=' + id + "&tipo=lloc");
    return false;
}


function TancarHistorial() {
    document.getElementById('PanelHistorial').style.visibility = 'hidden';
    return false;
}




function MostrarCamviarImgPerfil() {
    document.getElementById('FonsCamviarImatge').style.visibility = 'visible';
    document.getElementById('CapaCamviarImatge').style.visibility = 'visible';
    $('#CapaCamviarImatge').html('Loading...');
    $('#CapaCamviarImatge').load(UrlBase() + 'usr/perfil/Img.aspx');
    //$('#CapaCamviarImatge').load(UrlBase() + 'usr/perfil/Img.htm');
    return false;
}


function TancarImgPerfil() {
    document.getElementById('loading').style.visibility = 'hidden';
    document.getElementById('BotoError').style.visibility = 'hidden';
    document.getElementById('FonsCamviarImatge').style.visibility = 'hidden';
    document.getElementById('CapaCamviarImatge').style.visibility = 'hidden';
    return false;
}



function isTouchDevice() {
    try {
        document.createEvent("TouchEvent");
        return true;
    } catch (e) {
        return false;
    }
}


function touchScroll(id) {
    if (isTouchDevice()) { //if touch events exist...
        var el = document.getElementById(id);
        var scrollStartPos = 0;

        document.getElementById(id).addEventListener("touchstart", function (event) {
            scrollStartPos = this.scrollTop + event.touches[0].pageY;
            event.preventDefault();
        }, false);

        document.getElementById(id).addEventListener("touchmove", function (event) {
            this.scrollTop = scrollStartPos - event.touches[0].pageY;
            event.preventDefault();
        }, false);
    }
}