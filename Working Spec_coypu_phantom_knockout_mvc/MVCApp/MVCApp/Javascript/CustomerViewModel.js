/// <reference path="../Scripts/knockout-3.2.0.debug.js" />
function Customer(firstName, lastName) {
    var self = this;
    self.firstName = ko.observable(firstName);
    self.lastName = ko.observable(lastName);
}


function CustomerViewModel() {
    var self = this;

    self.customers = ko.observableArray([new Customer("Andy","cap")]);

    self.addCustomer = function() {
        self.customers.push(new Customer("a", "b"));
    };
    self.removeCustomer = function(customer) { self.customers.destroy(customer); };

}

ko.applyBindings(new CustomerViewModel());