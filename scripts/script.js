function validateForm() {

    var question = document.forms["myForm"]["question"].value;
    var x = document.forms["myForm"]["choix1"].value;
    var y = document.forms["myForm"]["choix2"].value;
    var z = document.forms["myForm"]["choix3"].value;
    var compte=0;


    if (x == '') { compte = compte + 1; }

    if (y == '') { compte = compte + 1; }

    if (z == '') { compte = compte + 1; }

    if (question == '')
    {
        alert("question manquante");
    }
    else if (compte > 1)
    {
        alert("Remplir au moins 2 choix");
    }
    else
    {
        document.forms["myForm"]["choix4"].value = "Remplissage OK";
    }
    document.forms["myForm"]["choix4"].value = compte;

}