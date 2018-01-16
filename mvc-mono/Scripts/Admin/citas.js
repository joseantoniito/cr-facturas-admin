// Write your Javascript code.

$(document).ready(function(){
	
	function load_items_citas(){

		function load_table_assistances(data){

			function create_listview(response, selector){

				var item_str = "citas";

				function create_data(response){
					var horarios = [];
					var interval = 60;
					var check_if_is_current = function(empleado, agendas){
			            var indexItem = null;
			            var itemInGrid = $.grep(agendas, function(item_agenda, index_agenda){ 
			                var ok = item_agenda.EmpleadoId == empleado.EmpleadoId;
			                if(ok) indexItem = index_agenda;
			                return ok;
			            });
			            return itemIndex != null;
					};

					for(i=8;i<=21;i++){
						var item_horario = {
							id: i,
							nombre: "{0}:00"
							hora: i,
							minuto: "00"
						};
						$.each(response.Empleados, function(index_empleado, item_empleado){
							var name = item_empleado.Nombre;
							var value = check_if_is_current(item_empleado, response.Agendas);
							item_horario[name] = value;
						});
						horarios.push(item_horario);
					}

					var citas = response.Citas;
					$.each(array, function(index_row, item_row){
		                var own_data = $.grep(citas, function(item_data){
		                    return item_data.Fecha.getHours() == item_row.id;//coinciden las horas (del renglon y la cita)
		                });
		                item_row.citas = own_data;
		            });

					return array;
				}

				function create_columns(response){
					var empleados = response.Empleados;

					var columns = [];
					$.each(empleados, function(index_empleado, item_empleado){
						var col = {
							field: item_empleado.Nombre,
							title: item_empleado.Nombre,
							template: "<div _ok='#: {0}#' class='action'></div>".format(item_empleado.Nombre)
						};



						columns.push(col);
					});
				}

				//INIT
				var data = create_data(response);
				var columns = create_columns(response);
				var data_bound = create_data_bound();
				create_grid(item_str, data, columns, data_bound)

				return;
	            var data = response.Citas;
	            var rows = create_horarios(data);
	            var columns = response.Empleados;
	            var template = '<tr _id="#:staffid#"><td>#:firstname#</td><td></td><td></td><td></td><td></tr>';
				$(selector).kendoListView({
	                dataSource: rows,
	                template: template,
	                dataBound: function(e) {
	                    var listview = this;
	                    $.each(e.sender.items(), function(index, item){
	                        item = $(item);
	                        var assistances = listview.dataItem(item).own_data;
	                         $.each(columns, function(indexD, itemD){
	                         	
	                            //var _check_type = itemD.EmpleadoId + '';
	                            //var inner_listview = create_inner_listview(assistances, _check_type);
	                            //item.find('td:nth-child({0})'.format(itemD.EmpleadoId + 1)).append(inner_listview);
	                        });
	                    });
	                    create_table_header();
	                }
	            });

			}



			function create_inner_listview(assistances, _check_type){
				var assistances_per_type = $.grep(assistances, function(item_a){
                    return item_a.type == _check_type;
                });

                var lv_assistances = $("<div></div>");
                lv_assistances.kendoListView({
                    dataSource: assistances_per_type,
                    template: 
                        '<div><strong>Hora: </strong><span id="check_date"> #:kendo.toString(new Date(check_date.replace(/-/g, "/")),"t")#</span><br>\
                            <strong>Lat: </strong><span id="latitude"> #:latitude#</span><br>\
                            <strong>Lng: </strong><span id="longitude"> #:longitude#</span><br>\
                        </div>',
                    dataBound: function(e){
                        var listview_assistances = this;
                        $.each(e.sender.items(), function(index, item){
                            item = $(item);
                            assistance_check = listview_assistances.dataItem(item);
                            
                            item.kendoTooltip({
                                autoHide: true,
                                content: 
                                kendo.template($("#check_assistance_map_template").html()),
                                width: 400,
                                height: 200,
                                position: "top",
                                show: function(e){
                                    if(marker_locations)
                                        marker_locations.setVisible(false); 

                                    var map = e.sender.content.find("#map_check_assistance");
                                    var latitude = e.sender.element.find("#latitude").text();
                                    var longitude = e.sender.element.find("#longitude").text();

                                    load_location(map, latitude, longitude);
                                }
                            });

                        });
                    }
                });
			}

			function create_table_header(){
				$("#table_roll").prepend("<tr id='t_header'></tr>");
	            $("#table_roll tr#t_header").append("<th _id='0'>ASESORES</th>");
	            $.each(list_check_types, function(indexD, itemD){
	                $("#table_roll tr#t_header").append("<th _id='{0}'>{1}</th>".format(itemD.id, itemD.name));
	            });
			}

			//INIT
	        $.post(admin_url + 'inventory/get_assistances',data).done(function(response) {
	            response = JSON.parse(response);
	            create_listview(response, "#listview_citas");
	        }).fail(function(data) {
	            alert_float('danger', data.responseText);
	        }); 
	    }


		//INIT
		create_grid_items();
		create_edit_window();
	}

	//INIT MASTER
	load_items_citas();			
});




