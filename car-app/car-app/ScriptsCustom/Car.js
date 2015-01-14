$(function () {

    $.connection.hub.url = "http://localhost:8080/signalr";
    var speedTrapperProxy = $.connection.speedTrapper; // the generated client-side hub proxy


    function init() {
        var car = {};
        car.Name = $("#Name").val();
        car.Speed = $("#Speed").val();

        return speedTrapperProxy.server.addCar(car).done(function (status) {
            if (status == "0") {
                onOverSpeed();
            }
        });
    }
    function onOverSpeed(car) {
        var car = {};
        car.Name = $("#Name").val();
        car.Speed = $("#Speed").val();
        var SpeedUnit = $("#SpeedUnit").val();
        $("#lblMessage").text("over speed " + car.Speed + " " + SpeedUnit).removeClass("speedBelowMessage").addClass("speedOverMessage");
        $("#lblSpeed").text("You're busted").removeClass("speedBelow").addClass("speedOver");
    }
    function speedApplied(speedLimit) {

        carSpeed = parseFloat($("#Speed").val());
        speedLimit = parseFloat(speedLimit);
        if (carSpeed > speedLimit) {
            onOverSpeed();
        }
    }
    // Add client-side hub methods that the server will call
    $.extend(speedTrapperProxy.client, {
        speedApplied: function (speed) {
            speedApplied(speed);
        },

    });

    // Start the connection
    $.connection.hub.start()
        .then(init)
        .done(function () {

        });
});