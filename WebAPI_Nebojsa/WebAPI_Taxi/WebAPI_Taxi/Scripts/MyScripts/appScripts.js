function loadLoginPage() {
    $("div#indexPageID").show();
    $("div#indexPageID").load("./Content/HTML/LogIn.html"); 
    $("#reg").bind('click', function () {
        if ($("#reg").text() == "Registration") {
            $("div#indexPageID").show();
            $("#indexPageID").load("./Content/HTML/Register.html");
            $("#reg").html("Login");
            $("#errDiv").hide();
        }
        else {
            $("div#indexPageID").show();
            $("#indexPageID").load("./Content/HTML/LogIn.html");
            $("#reg").html("Registration");
            $("#errDiv").hide();
        }

        return false;
    });
}

function handleLogin() {
    $.post('api/Korisnici/Prijava', $("#logInForm").serialize(), "json")
        .done(function (data, status, xhr) {
            $("#reg").hide();
            //localStorage.setItem("ulogovan", data.KorisnikID)
            //$("div#errdiv").hide();
        })
        .fail(function (jqXHR) {
            $("div#errdiv").text(jqXHR.responseJSON["Message"]).show();
        });
}

function handleRegistration() {
    $.post('api/Korisnici/PostRegister', $("#regForm").serialize(), "json")
        .done(function (data, status, xhr) {
            alert(data);
        })
        .fail(function (jqXHR, textStatus) {
            alert(jqXHR.responseJSON["Message"]);
        });
}


function validateLogin() {
    $("#logInForm").validate({
        rules: {
            usernameID: {
                required: true,
                minlength: 4
            },
            passwordID: {
                required: true,
                minlength: 5
            }
        },
        messages: {
            usernameID: {
                required: "Username is required",
                minlength: "Username must be at least 4 characters long"
            },
            passwordID: {
                required: "Password is required",
                minlength: "Password must be at least 5 characters long"
            }
        },
        submitHandler: function (form) { handleLogin(); }
           
    });
}

function validateRegister() {
    $("#regForm").validate({
        rules: {
            usernameRegID: {
                required: true,
                minlength: 4
            },
            passwordRegID: {
                required: true,
                minlength: 5
            },
            password2RegID: {
                required: true,
                minlength: 5
            },
            nameRegID: "required",
            surnameRegID: "required",
            emailRegID: {
                email: true
            },
            idNumberRegID: {
                required: true,
                number: true,
                minlength: 13,
                maxlength: 13
            },
            telephoneRegID: {
                number: true
            }  
        },
        messages: {
            usernameRegID: {
                required: "Username is required",
                minlength: "Username must be at least 4 characters long"
            },
            passwordRegID: {
                required: "Password is required",
                minlength: "Password must be at least 5 characters long"
            },
            password2RegID: {
                required: "Confirm password is required",
                equalto: "Confirmed password must be same as password"
            },
            nameRegID: {
                required: "Name is required"
            },
            surnameRegID: {
                required: "Surname is required"
            },
            emailRegID: {
                email: "Email address must be valid"
            },
            idNumberRegID: {
                required: "ID number is required",
                number: "You must enter the number",
                minlength: "ID number length must be 13 digits",
                maxlength: "ID number length must be 13 digits"
            },
            telephoneRegID: {
                number: "You must enter the number"
            }
        },

        submitHandler: function (form) { handleRegistration(); }
    });
}