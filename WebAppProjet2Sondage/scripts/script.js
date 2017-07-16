function validateCreateForm()
{
    var question = document.forms["myForm"]["question"].value;
    var w = document.forms["myForm"]["choix1"].value;
    var x = document.forms["myForm"]["choix2"].value;
    var y = document.forms["myForm"]["choix3"].value;
    var z = document.forms["myForm"]["choix4"].value;
    var compte=0;

    if (w == '') { compte = compte + 1; }

    if (x == '') { compte = compte + 1; }

    if (y == '') { compte = compte + 1; }

    if (z == '') { compte = compte + 1; }

    if (question == '')
    {
        alert("question manquante");
        return false;
    }
    else if (compte > 2)
    {
        alert("Remplir au moins 2 choix");
        return false;
    }
    else
    {
        //alert("Remplissage OK");
        return true;
    }
}

function validateVoteForm()
{
    //vérifie si un truc est coché!
    if(typeof $(".checkElement:checked").val() == "undefined")
    {
        alert("Cochez!");
        return false;
    }
    else
    {
        return true;
    }
}
