// Class to represent a row in the seat reservations grid
function SeatReservation(ReservationID, name, initialMeal) {
    var self = this;
    self.ID = ReservationID;
    self.name = ko.observable(name);
    self.meal = ko.observable(initialMeal);

    self.formattedPrice = ko.computed(function () {
        var price = self.meal().price;
        return price ? "$" + price.toFixed(2) : "None";
    });

    // name.subscribe - subscribe - when Name textbox change
    self.name.subscribe(function (newName) {
        // creat new reservation data (res)
        var res = {
            "ReservationID": self.ID,
            "name": newName, // new name
            "initialMeal": self.meal().mealId
        }
        $.ajax({
            url: "/Home/SaveReservation",
            type: "POST",
            dataType: "json",
            data: res, // post data (res) to controller
            success: function (result) {
                // get new ID from controller, and change ID value in js
                self.ID = result.ReservationID;
            },
            error: function () { }
        });
    });

    // meal.subscribe - subscribe - when Meal dropdown option changes
    self.meal.subscribe(function (newMeal) {
        // creat new reservation data (res)
        var res = {
            "ReservationID": self.ID,
            "name": self.name(),
            "initialMeal": newMeal.mealId // id of new meal
        }
        $.ajax({
            url: "/Home/SaveReseration",
            type: "POST",
            dataType: "json",
            data: res, // post data (res)
            success: function (result) {
                self.ID = result.ReservationID;
            },
            error: function () { }
        });
    });
}

// Overall viewmodel for this screen, along with initial state
function ReservationsViewModel() {
    var self = this;

    // Non-editable catalog data - would come from the server
    self.availableMeals = [
        { mealId:0, mealName: "Standard (sandwich)", price: 10 },
        { mealId:1, mealName: "Premium (lobster)", price: 34.95 },
        { mealId:2, mealName: "Ultimate (whole zebra)", price: 290}
    ];

    // Call Homecontroller, action GetAllReservations
    self.seats = ko.observableArray();
    $.ajax({
        url: "/Home/GetAllReservations",
        type: "GET",
        datatype: "json",
        success: function (result) {
            // get "result" from /Home/GetAllReservations controller
            for (var i = 0; i < result.length; i++) {
                var reservationInLoop = new SeatReservation(
                    // ReservationID, name, initialMeal is the name of column in Database
                    result[i].ReservationID,
                    result[i].name,
                    self.availableMeals[result[i].initialMeal]
                );
                // add new item in seats array/list
                self.seats.push(reservationInLoop);
            }
        },
        error: function () { }
    });

    self.addSeat = function () {
        // reserve ID = -1 for new created seat. Because -1 not exist in current Database
        // controller will change id to a new valid value.
        self.seats.push(new SeatReservation(-1,"", self.availableMeals[0]));
    }
    

    self.removeSeat = function (seat){
        self.seats.remove(seat);

        $.ajax({
            url: "/Home/DeleteReservation/"+seat.ID,
            type: "GET",
            datatype: "json",
            success: function (result) {
                // delete success
            },
            error: function () { }
        });
    }

    self.totalSurcharge = ko.computed(function () {
        var total = 0;
        for (var i = 0; i < self.seats().length; i++)
            total += self.seats()[i].meal().price;
        return total;
    });
}

ko.applyBindings(new ReservationsViewModel());