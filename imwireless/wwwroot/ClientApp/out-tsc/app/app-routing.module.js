"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var customers_component_1 = require("./customers/customers.component");
var customers_grid_component_1 = require("./customers/customers-grid.component");
var customer_edit_component_1 = require("./customers/customer-edit.component");
var customer_edit_reactive_component_1 = require("./customers/customer-edit-reactive.component");
var routes = [
    { path: 'customers', component: customers_component_1.CustomersComponent },
    { path: 'customers/:id', component: customer_edit_component_1.CustomerEditComponent },
    //{ path: 'customers/:id', component: CustomerEditReactiveComponent },
    { path: '**', pathMatch: 'full', redirectTo: '/customers' } //catch any unfound routes and redirect to home page
];
var AppRoutingModule = /** @class */ (function () {
    function AppRoutingModule() {
    }
    AppRoutingModule.components = [customers_component_1.CustomersComponent, customers_grid_component_1.CustomersGridComponent, customer_edit_component_1.CustomerEditComponent, customer_edit_reactive_component_1.CustomerEditReactiveComponent];
    AppRoutingModule = __decorate([
        core_1.NgModule({
            imports: [router_1.RouterModule.forRoot(routes)],
            exports: [router_1.RouterModule]
        })
    ], AppRoutingModule);
    return AppRoutingModule;
}());
exports.AppRoutingModule = AppRoutingModule;
//# sourceMappingURL=app-routing.module.js.map