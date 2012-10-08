/// <reference path="../jquery-1.7.2-vsdoc.js" />
/// <reference path="../knockout-2.1.0.debug.js" />
/// <reference path="../bootstrap.js" />
/// <reference path="../jquery.validate.js" />
/// <reference path="../jquery.validate.unobtrusive.js" />

(function () {
    
    var vm = {};
    var apiBaseAddress = "/api/cars";
    var authApiBaseAddress = "/api/auth";

    function Car() { 

        var self = this;

        self.Id = ko.observable();
        self.Make = ko.observable();
        self.Model = ko.observable();
        self.Year = ko.observable();
        self.Price = ko.observable();
    }

    function buidCar(carJsonObj) { 

        return new Car()
            .Id(carJsonObj.Id)
            .Make(carJsonObj.Make)
            .Model(carJsonObj.Model)
            .Year(carJsonObj.Year)
            .Price(carJsonObj.Price);
    }

    function openAuthDialogBox() { 
        $("div.auth-dialog").modal({ keyboard: false });
    }

    function closeAuthDialogBox() { 
        $("div.auth-dialog").modal("hide");
    }

    function retrieveAndBindCars() { 

        $.ajax({
            url: apiBaseAddress,
            type: "GET",
            contentType: "application/json; charset=utf-8",
            statusCode: { 
                //OK
                200: function(result) { 
                    $.each(result, function(i, data) { 
                        vm.cars.push(buidCar(data));
                    });
                },
                //Unauthorized
                401: function() { 
                    vm.isAuthorized(false);
                    openAuthDialogBox();
                }
            }
        });
    }

    vm.cars = ko.observableArray([]);
    vm.isAuthorized = ko.observable(true);
    ko.applyBindings(vm);

    $(function () { 

        $(window).on("click", "a.auth-dialog-invoker", function(e) { 
            openAuthDialogBox();
            e.preventDefault();
        });

        retrieveAndBindCars();

        $("form").submit(function(e) { 
            var $form = $(this);

            if($form.valid()) { 

                var userName = $form.find("#UserName").val();
                var password = $form.find("#Password").val();
                
                $.ajax({
                    url: authApiBaseAddress,
                    type: "POST",
                    data: JSON.stringify({ UserName: userName, password: password }),
                    contentType: "application/json; charset=utf-8",
                    statusCode: { 
                        //OK
                        200: function(result) { 
                            vm.isAuthorized(true);
                            closeAuthDialogBox();
                            retrieveAndBindCars();
                        },
                        401: function(result) { 
                            alert(result.responseText);
                        }
                    }
                });
            }

            e.preventDefault();
        });
    });

}());