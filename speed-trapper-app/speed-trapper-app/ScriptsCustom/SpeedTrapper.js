$(function () {

    var speedTrapperProxy = $.connection.speedTrapper; // the generated client-side hub proxy

    var $carTable = $('#carTable');
    var $carTableRow = $carTable.find('tr');

    function formatCar(car) {
        return $.extend(car, {
            SpeedClass: car.Speed > car.MaxSpeed ? 'speedOver' : 'speedBelow'
        });
    }
      
    function addCar(car) {
        var car = formatCar(car);
        $carTableRow.append('<td><span class="carName">' + car.Name + '</span> <br /> <span class="' + car.SpeedClass + '">' + car.Speed + $("#SpeedUnit").val()+'</span></td>');
    }
    function validateInput() {

        var validationResult = true;
        var speed = $("#txtSpeed").val().trim();
        if (speed=='') {
            validationResult = false;
        }
        if (isNaN(speed)) {
          
            validationResult = false;
        }
        if (speed.indexOf('.')>=0) {
            validationResult = false;
        }
        if (!validationResult) {
            setErrorMessage("Invalid Speed.Speed should be integer");
           
        }
        return validationResult;
    }

    function speedApplied() {
        $("#btnApply").show(500);
    }

    function  setSuccessMessage(message) {
        $("#lblMsg").text(message).removeClass("errMessage").addClass("succMessage");
        setTimeout(function () { $("#lblMsg").text(''); }, 2000);
    }

    function setErrorMessage(message) {
        $("#lblMsg").text(message).addClass("errMessage").removeClass("succMessage");
        setTimeout(function () { $("#lblMsg").text(''); }, 3000);
    }
    // Add client-side hub methods that the server will call
    function init() {
        return speedTrapperProxy.server.getOverspeed().done(function (speed) {
            $("#txtSpeed").val(speed)
        });
    }
    $.extend(speedTrapperProxy.client, {
        carAdded: function (car) {
            addCar(car);
        }, speedApplied: function (speed) {
            $("#txtSpeed").val(speed);
            setSuccessMessage('Speed applied successfully!!!');
            speedApplied();
        }
        , speedAppliedError: function (message) {
            setErrorMessage(message);
            speedApplied();
        }
    });

    $.connection.hub.error(function (error) {
        setErrorMessage("Server error!!!");
    });
 
    // Start the connection
    $.connection.hub.start()
        .then(init)
        .done(function () {

            $("#btnApply").click(function () {
                if (validateInput()) {
                    var speed = $("#txtSpeed").val();
                    $(this).hide();
                    $("#lblMsg").text('Please wait....');
                    speedTrapperProxy.server.applySpeed(speed).fail(function (error) {
                        $("#btnApply").show();
                        setErrorMessage("Server error!!!");
                    });;
                }

            });

        });
});