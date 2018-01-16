// Write your Javascript code.


		String.prototype.format = function() {
			var str = this;
			for (var i = 0; i < arguments.length; i++) {       
				var reg = new RegExp("\\{" + i + "\\}", "gm");             
				str = str.replace(reg, arguments[i]);
			}
			return str;
		}

		function isJson(str) {
			try {
				JSON.parse(str);
			} catch (e) {
				return false;
			}
			return true;
		}

		jQuery(document).ready(function(){
			//INIT MASTER
			$(".fa-remove.pull-right").on("click",function(){
				window.location.replace(document.referrer);
			})
			load_items_empresas();			

			var load_empresa = function () {
				if($("#tabstrip-empresa").length == 0) return;

		        $("#tabstrip-empresa").kendoTabStrip({
		            tabPosition: "left",
		            animation: { open: { effects: "fadeIn" } }
		        });

		        load_items_categorias();
		        load_items_empleados();
		    }
		    load_empresa();
		});
			function load_items_empresas(){
				var item_str = "empresas";

				function create_grid_items(){  
					var grid;
					var columns = [
						{field: "Nombre", title: "Nombre"},
						{field: "Telefono", title: "Telefono", template: "<span id='lblTelefono'>#:Telefono#</span>"},
						{field: "Direccion", title: "Dirección"},
						{field: "Status", title: "Status"},
						{field:"EmpresaId", title:"Acciones", width:"100px", 
						template: "<i _id='#:EmpresaId#' id='btn_editar' class='fa fa-pencil-square' title='Editar'></i>\
									<a  id='btn_ver' href='/Admin/Empresa/#:EmpresaId#'><i class='fa fa-eye' title='Ver'></i></a>"}
					];

					function data_bound_items(e){
						jQuery.each(e.sender.items(), function(index, item){
							item = jQuery(item);
							item.find("#btn_editar").on("click", edit_item);
						});
					}; 

					function edit_item(event){
						console.log("entro a editar");
						var sender = jQuery(event.currentTarget);
						var id = sender.attr("_id");
						jQuery("[name='EmpresaId']").val(id);
						
						var item = sender.parent().parent();
						var data = jQuery("#grid_"+item_str).data("kendoGrid").dataItem(item);
						console.log("data user edit" , data);

						response = data;
						jQuery("[name='Nombre']").val(response.Nombre);
						jQuery("[name='Telefono']").val(response.Telefono);
						jQuery("[name='Direccion']").val(response.Direccion);
						jQuery("[name='Status']").val(response.Status);

						jQuery("#window_"+item_str).data("kendoWindow").center().open();
					}

					//INIT
					jQuery.get('/User/ObtenerEmpresas/', function(response) {
						grid = create_grid(item_str, response, columns, data_bound_items);
					}, 'json');

				}

				function create_edit_window(){
					var window, validator;

					function clear_controls(){
						
						jQuery("[name='EmpresaId']").val("0");
						jQuery("[name='Nombre']").val("");
						jQuery("[name='Telefono']").val(""); 
						jQuery("[name='Direccion']").val("");
						jQuery("[name='Status']").val("");
					}

					function register_item(){
						// EmpresaId, Nombre, Telefono, Direccion, Status
						if(!validator.validate()) return false;
						var url = '/User/GuardarEmpresa';
						var data = {
							EmpresaId : jQuery("[name='EmpresaId']").val(),
							Nombre : jQuery("[name='Nombre']").val(), 
							Telefono : jQuery("[name='Telefono']").val(), 
							Direccion : jQuery("[name='Direccion']").val(),
							Status: jQuery("[name='Status']").val()
						};
						var fn_compare_id = function(item, response){
								return item.EmpresaId == response.EmpresaId;
							}
						post_url(url, data, item_str, fn_compare_id);
						return false;
					}

					//INIT
					window = create_window_controls(item_str, clear_controls);
					validator = load_form_controls(item_str, register_item)
				}

				//INIT
				if(jQuery("#grid_{0}".format(item_str)).length == 0) return;
				create_grid_items();
				create_edit_window();
			}



			function load_items_categorias(){
				var item_str = "categorias";
				var form = jQuery("#window_{0} form".format(item_str));

				function create_grid_items(){  
					var grid;
					var columns = [
						{field: "Nombre", title: "Nombre"},
						{field: "CategoríaPadreId", title: "CategoríaPadreId", template: "<span id='lblCategoríaPadreId'>#:CategoríaPadreId#</span>"},
						{field:"CategoriaId", title:"Acciones", width:"100px", 
						template: "<i _id='#:CategoriaId#' id='btn_editar' class='fa fa-pencil-square' title='Editar'></i>"}
					];

					function data_bound_items(e){
						jQuery.each(e.sender.items(), function(index, item){
							item = jQuery(item);
							item.find("#btn_editar").on("click", edit_item);
						});
					}; 

					function edit_item(event){
						console.log("entro a editar");
						var sender = jQuery(event.currentTarget);
						var id = sender.attr("_id");
						jQuery("[name='EmpresaId']").val(id);
						
						var item = sender.parent().parent();
						var data = jQuery("#grid_"+item_str).data("kendoGrid").dataItem(item);
						console.log("data edit" , data);

						response = data;
						form.find("[name='CategoriaId']").val(response.CategoriaId);
						form.find("[name='Nombre']").val(response.Nombre);
						form.find("[name='CategoríaPadreId']").val(response.CategoríaPadreId);
						form.find("[name='EmpresaId']").val(response.EmpresaId);

						jQuery("#window_"+item_str).data("kendoWindow").center().open();
					}

					//INIT
					jQuery.get('/User/ObtenerCategorias/', function(response) {
						grid = create_grid(item_str, response, columns, data_bound_items);
					}, 'json');
				}

				function create_edit_window(){
					var window, validator;

					function clear_controls(){
						form.find("[name='CategoriaId']").val("0");
						form.find("[name='Nombre']").val("");
						form.find("[name='CategoríaPadreId']").val(""); 
						form.find("[name='EmpresaId']").val("");
					}

					function register_item(){
						if(!validator.validate()) return false;
	
						var url = '/User/GuardarCategoria';
						var data = {
							CategoriaId : form.find("[name='CategoriaId']").val(),
							Nombre : form.find("[name='Nombre']").val(), 
							CategoríaPadreId : form.find("[name='CategoríaPadreId']").val(), 
							EmpresaId : form.find("[name='EmpresaId']").val()
						};
						var fn_compare_id = function(item, response){
								return item.CategoriaId == response.CategoriaId;
							}
						post_url(url, data, item_str, fn_compare_id);
						return false;
					}


					window = create_window_controls(item_str, clear_controls);
					validator = load_form_controls(item_str, register_item)
				}

				//INIT
				if(jQuery("#grid_{0}".format(item_str)).length == 0) return;
				create_grid_items();
				create_edit_window();
			}



			function load_items_empleados(){
				var item_str = "empleados";
				var form = jQuery("#window_{0} form".format(item_str));

				function create_grid_items(){  
					var grid;
					var columns = [
						{field: "Nombre", title: "Nombre"},
						{field: "Apellido1", title: "Apellido 1"},
						{field: "Apellido2", title: "Apellido 2"},
						{field: "Telefono1", title: "Teléfono 1"},
						{field: "Telefono2", title: "Teléfono 2"},
						//{field: "Identificacion", title: "Identificacion"},
						//{field: "Foto", title: "Foto"},
						//{field: "Horario", title: "Horario"},
						//{field: "TipoPago", title: "TipoPago"},
						{field: "Puesto", title: "Puesto"},
						{field: "Status", title: "Status"},
						{field:"EmpleadoId", title:"Acciones", width:"100px", 
						template: "<i _id='#:EmpleadoId#' id='btn_editar' class='fa fa-pencil-square' title='Editar'></i>"}
					];

					function data_bound_items(e){
						jQuery.each(e.sender.items(), function(index, item){
							item = jQuery(item);
							item.find("#btn_editar").on("click", edit_item);
						});
					}; 

					function edit_item(event){
						console.log("entro a editar");
						var sender = jQuery(event.currentTarget);
						var id = sender.attr("_id");
						form.find("[name='EmpleadoId']").val(id);
						
						var item = sender.parent().parent();
						var data = jQuery("#grid_"+item_str).data("kendoGrid").dataItem(item);
						console.log("data edit" , data);

						response = data;
						form.find("[name='EmpleadoId']").val(response.EmpleadoId);
						form.find("[name='EmpresaId']").val(response.EmpresaId);
						form.find("[name='Nombre']").val(response.Nombre);
						form.find("[name='Apellido1']").val(response.Apellido1);
						form.find("[name='Apellido2']").val(response.Apellido2);
						form.find("[name='Telefono1']").val(response.Telefono1);
						form.find("[name='Telefono2']").val(response.Telefono2);
						form.find("[name='Apodo']").val(response.Apodo);
						form.find("[name='Identificacion']").val(response.Identificacion);
						form.find("[name='Foto']").val(response.Foto);
						form.find("[name='Horario']").val(response.Horario);
						form.find("[name='TipoPago']").val(response.TipoPago);
						form.find("[name='Puesto']").val(response.Puesto);
						form.find("[name='Status']").val(response.Status);

						jQuery("#window_"+item_str).data("kendoWindow").center().open();
					}

					//INIT
					jQuery.get('/User/ObtenerEmpleados/', function(response) {
						grid = create_grid(item_str, response, columns, data_bound_items);
					}, 'json');
				}

				function create_edit_window(){
					var window, validator;

					function clear_controls(){
						form.find("[name='EmpleadoId']").val("0");
						form.find("[name='EmpresaId']").val("");
						form.find("[name='Nombre']").val(""); 
						form.find("[name='Apellido1']").val("");
						form.find("[name='Apellido2']").val("");
						form.find("[name='Telefono1']").val("");
						form.find("[name='Telefono2']").val("");
						form.find("[name='Apodo']").val("");
						form.find("[name='Identificacion']").val("");
						form.find("[name='Foto']").val("");
						form.find("[name='Horario']").val("");
						form.find("[name='TipoPago']").val("");
						form.find("[name='Puesto']").val("");
						form.find("[name='Status']").val("");
					}

					function register_item(){
						if(!validator.validate()) return false;
	
						var url = '/User/GuardarEmpleado';
						var data = {
							EmpleadoId : form.find("[name='EmpleadoId']").val(),
							EmpresaId : form.find("[name='EmpresaId']").val(), 
							Nombre : form.find("[name='Nombre']").val(), 
							Apellido1 : form.find("[name='Apellido1']").val(),
							Apellido2 : form.find("[name='Apellido2']").val(), 
							Telefono1 : form.find("[name='Telefono1']").val(), 
							Telefono2 : form.find("[name='Telefono2']").val(), 
							Apodo : form.find("[name='Apodo']").val(), 
							Identificacion : form.find("[name='Identificacion']").val(), 
							Foto : form.find("[name='Foto']").val(), 
							Horario : form.find("[name='Horario']").val(), 
							TipoPago : form.find("[name='TipoPago']").val(),
							Puesto : form.find("[name='Puesto']").val(), 
							Status : form.find("[name='Status']").val(),  
						};
						var fn_compare_id = function(item, response){
								return item.EmpleadoId == response.EmpleadoId;
							}
						post_url(url, data, item_str, fn_compare_id);
						return false;
					}


					window = create_window_controls(item_str, clear_controls);
					validator = load_form_controls(item_str, register_item)
				}

				//INIT
				if(jQuery("#grid_{0}".format(item_str)).length == 0) return;
				create_grid_items();
				create_edit_window();
			}






		    //UTILERIAS
		    function create_grid(item_str, response, columns, data_bound_items){
				console.log(response);
				jQuery("#grid_{0}".format(item_str)).kendoGrid({
					dataSource: response,
					columns: columns,
					dataBound: data_bound_items
				});
				var grid = jQuery("#grid_{0}".format(item_str)).data("kendoGrid");
				return grid;
			}

			function refresh_grid(item_str, response, fn_compare_id, delete_item){
				grid = jQuery("#grid_{0}".format(item_str)).data("kendoGrid");
				
				if(!grid) return;
				
				var data = grid.dataSource.data();
				var indexItem = null;
				var itemInGrid = jQuery.grep(data, function(item, index){ 
					var ok = fn_compare_id(item, response);
					if(ok) indexItem = index;
					return ok;
				});
				
				if(indexItem == null){
					data.unshift(response);
				}
				else{
					if(!delete_item)
						data.splice(indexItem, 1, response);
					else
						data.splice(indexItem, 1);
				}
				grid.dataSource.data(data);
			}

			function create_window_controls(item_str, clear_controls){
				jQuery("#window_{0}".format(item_str)).kendoWindow({
					width: "600px",
					title: "",
					visible: false,
					modal: true,
					resizable: false,
					scrollable: false,
					close: clear_controls
				});
				var window = jQuery("#window_{0}".format(item_str)).data("kendoWindow");
				jQuery("#btn_add_{0}".format(item_str)).off("click");
				jQuery("#btn_add_{0}".format(item_str)).on("click", function(sender){
					window.center().open();
				});

				jQuery("#btn_close_window_{0}".format(item_str)).on("click", function(){
			 		window.close();
				});

				return window;
			}

			function load_form_controls(item_str, register_item){
				
				var form = jQuery("#window_{0} form".format(item_str));
				jQuery("#window_{0} form button[type=submit]".format(item_str)).on("click", register_item);
				validator = form.kendoValidator().data("kendoValidator");
				return validator;
			}

			function post_url(url, data, item_str, fn_compare_id){
				$.ajax({
		            type: 'POST',
		            url: url,
		            dataType: 'json',
		            contentType: 'application/json; charset=utf-8',
		            data: JSON.stringify(data),
		            success: function (response) {
		                console.log('Respuesta recibida: ', response);
		                if(response){
							alert("Registro actualizado con éxito.");
							console.log(response, 'response post');
							var window_form = jQuery("#window_{0}".format(item_str)).data("kendoWindow");

							if(window_form){
								window_form.close();
								refresh_grid(item_str, response, fn_compare_id);
							}
							else
								window.location.replace(document.referrer);

							
						}
						else{
							alert("Ocurrió un error.")
						}
		            },
		            error: function(err){
		            	console.log("error post", err);
		            }
		        });
			}

			function get_id_from_url(){
				var array = document.URL.split('/');
				var value = array.slice(-1).pop();
				if( isNaN(value) ) value = 0;
				return value;
			}

			function get_data_object_from_form(selector){
				var paramObj = {};
				$.each($(selector).serializeArray(), function(_, kv) {
				  paramObj[kv.name] = kv.value;
				});
				return paramObj;
			}

			function set_data_object_to_form(selector, data){
				var inputs = $( "{0} input[name]".format(selector) );
				$.each(inputs, function(index, item){
					item = $(item);
					item.val( data[ item.attr("name") ] );
				});
			}


$(document).ready(function () {

    console.log("hola mundo");



    //registro
    var validator_registro;
    var init_user_form = function () {
        $("#register_container form button[type='submit']")
            .on("click", manage_user);
        validator_registro = $("#register_container form")
            .kendoValidator().data("kendoValidator");

        $("#login_container form button[type='submit']").on("click", login_user);

        $("#log_out").on("click", logout_user);

        if ($("#register_container").length > 0)
            $("body").addClass("register_page");

        if ($("#login_container").length > 0)
            $("body").addClass("login_page");
    }

    var manage_user = function () {
        if (!validator_registro.validate()) return false;
        data = {
            "Nombre": $("[name='Nombre']").val(),
            "Correo": $("[name='Correo']").val(),
            "Telefono": $("[name='Telefono']").val(),
            "Clave": $("[name='Clave']").val(),
            "CedulaJuridica": $("[name='CedulaJuridica']").val(),
            "NombreRepresentanteLegal": $("[name='NombreRepresentanteLegal']").val(),
            "CedulaRepresentanteLegal": $("[name='CedulaRepresentanteLegal']").val()
        };
        $.ajax({
            type: 'POST',
            url: '/User/GuardarEmpresa',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data),
            success: function (result) {
                console.log('Respuesta recibida: ');
                console.log(result);
                alert("Empresa generada con éxito.");
            }
        });
        return false;
    }

    var login_user = function () {
    	debugger;
        data = {
            "Login": $("[name='Email']").val(),
            "Clave": $("[name='PasswordHash']").val()
        };
        dataType = 'application/json; charset=utf-8';
        $.ajax({
            type: 'POST',
            url: '/User/LogIn',
            dataType: 'json',
            contentType: dataType,
            data: JSON.stringify(data),
            success: function (result) {
                console.log('Log In Request');
                console.log(result);
                if (result)
                    window.location.replace("/Admin/Default");//Dashboard
                else
                    alert("Sus credenciales son inválidas.");
            }
        });
        return false;
    }

    var logout_user = function () {

        jQuery.get('/User/Logout', function (response) {
            console.log(response);
            window.location.replace("/Admin/Dashboard/#");
            var ok = response == 1;
            console.log("Log-out: ", ok);
        });//, 'json'
    }

    init_user_form();

    //admin
    /*var rutas;
    var load_dashboard = function () {
        $("#tabstrip-dashboard").kendoTabStrip({
            tabPosition: "left",
            animation: { open: { effects: "fadeIn" } }
        });

        load_rutas();
        load_generar_codigo_ruta();
        load_generar_codigo_qr();
        load_movimientos_fecha();
    }

    var load_rutas = function () {
        if ($("#grid_rutas").length == 0) return;

        var create_grid = function (rensponse) {
            $("#grid_rutas").kendoGrid({
                pageable: true,
                scrollable: false,
                width: 900,
                dataSource: { data: rensponse, pageSize: 10 },
                columns: [
                    { field: "NombreRuta", title: "Nombre Ruta" },
                    { field: "Status", title: "Status" },
                    { field: "Codigo", title: "Codigo" },
                    { field: "Descripcion", title: "Descripcion Tarifa" },
                    { field: "Monto", title: "Monto Tarifa" }
                ],
                dataBound: function (e) {
                    console.log("dataBound");

                    $.each(e.sender.items(), function (index, item) {
                        $(item).find("#btn_edit")
                            .on("click", function () { });
                    });
                }
            }).data("kendoGrid");
        }

        console.log("load:rutas OK");
        jQuery.get('/User/ObtenerRutas/', function (response) {
            console.log(response);
            create_grid(response);
            rutas = response;
            load_generar_codigo_qr();
        }, 'json');

    }

    var load_generar_codigo_ruta = function () {
        if ($("#grid_codigo_rutas").length == 0) return;

        var create_grid = function (rensponse) {
            $("#grid_codigo_rutas").kendoGrid({
                pageable: true,
                scrollable: false,
                width: 900,
                dataSource: { data: rensponse, pageSize: 10 },
                columns: [
                    { field: "NombreRuta", title: "Nombre Ruta" },
                    { field: "Status", title: "Status" },
                    { field: "Codigo", title: "Codigo" },
                    { field: "RutaId", title: "Acciones", 
                        template: "<span id='btn_generar' class='btn btn-info'>Generar Codigo</span>"
                    }
                ],
                dataBound: function (e) {
                    console.log("dataBound");
                    var grid = e.sender;

                    $.each(grid.items(), function (index, item) {
                        var dataItem = grid.dataItem(item);

                        $(item).find("#btn_generar")
                            .on("click", function () {

                                jQuery.get('/User/GenerarCodigoRuta/' + dataItem.RutaId, function (response) {
                                    console.log(response);
                                    alert(response);
                                    dataItem.Codigo = response;

                                    refresh_grid($("#grid_codigo_rutas"), dataItem);
                                    //hacer el refresh del grid
                                });
                        });
                    });
                }
            }).data("kendoGrid");
        }

        var refresh_grid = function (grid_element, response, delete_item) {
            grid = grid_element.data("kendoGrid");

            if (!grid) return;

            var data = grid.dataSource.data();
            var indexItem = null;
            var itemInGrid = jQuery.grep(data, function(item, index){ 
                var ok = item.RutaId == response.RutaId;
                if(ok) indexItem = index;
                return ok;
            });

            if (indexItem == null) {
                data.push(response);
            }
            else {
                if (!delete_item)
                    data.splice(indexItem, 1, response);
                else
                    data.splice(indexItem, 1);
            }
            grid.dataSource.data(data);
        }

        console.log("load:rutas OK");
        jQuery.get('/User/ObtenerRutas/', function (response) {
            console.log(response);
            create_grid(response);
        }, 'json');

    }

    var load_generar_codigo_qr = function () {
        $("#dropdown_rutas").kendoDropDownList({
            dataTextField: "NombreRuta",
            dataValueField: "RutaId",
            dataSource: rutas,
            optionLabel: "Rutas",
            change: function (e) { },
            value: null
        });

        $("#btn_generar_codigo_qr").on("click", function () {
            var ruta_id = $("#dropdown_rutas").val();
            
            var ruta = $.grep(rutas, function (item_r) {
                return item_r.RutaId == ruta_id;
            })[0];

            var valor_qr = ruta.Codigo;
            $("#qr_ruta").html("");
            $("#qr_ruta").kendoQRCode({
                value: valor_qr,
                size: 120,
                color: "#e15613",
                background: "transparent"
            });
        });
    }

    var load_movimientos_fecha = function () {
        if ($("#grid_movimientos").length == 0) return;

        var create_grid = function (rensponse) {
            $("#grid_movimientos").kendoGrid({
                pageable: true,
                scrollable: false,
                //width: 900,
                dataSource: { data: rensponse, pageSize: 10 },
                columns: [
                    { field: "Monto", title: "Monto" },
                    //{ field: "Registered", title: "Registered" },
                    { field: "CantidadPasajes", title: "Pasajes" },
                    //{ field: "Validacion", title: "Validacion" },
                    { field: "TipoMovimiento", title: "Tipo Movimiento" },
                    { field: "MontoActual", title: "Monto Actual" },
                    { field: "NombreRuta", title: "Nombre Ruta" }
                ],
                dataBound: function (e) {
                    console.log("dataBound");

                    $.each(e.sender.items(), function (index, item) {
                        $(item).find("#btn_edit")
                            .on("click", function () { });
                    });
                }
            }).data("kendoGrid");
        }

        console.log("load:movimientos OK");
        jQuery.get('/User/ObtenerMovimientos/null_null', function (response) {
            console.log(response);
            create_grid(response);
        }, 'json');

        if ($("#search_movimientos").length == 0) return;

        var s = $("#search_movimientos");
        s.find("[name='initial_date']").kendoDatePicker({ format: "yyyy-MM-dd" });
        s.find("[name='final_date']").kendoDatePicker({ format: "yyyy-MM-dd" });

        s.find("#search").on("click", function () {
            var initial_date = s.find("[name='initial_date']").val();
            var final_date = s.find("[name='final_date']").val();
            initial_date = initial_date == '' ? 'null' : initial_date;
            final_date = final_date == '' ? 'null' : final_date;
            jQuery.get('/User/ObtenerMovimientos/' + initial_date + '_' + final_date, function (response) {
                console.log(response);
                create_grid(response);
            }, 'json');
            return false;
        });
    }
    */
    //load_dashboard();

});
