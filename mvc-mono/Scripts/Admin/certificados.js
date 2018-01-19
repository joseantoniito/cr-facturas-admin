﻿// Write your Javascript code.

$(document).ready(function(){
	
	function load_items_certificados(){
		var item_str = "certificados";
		var form = $("#window_{0} form".format(item_str));

		function create_grid_items(){  
			if($("#grid_{0}".format(item_str)).length == 0) return;
			var grid;
			//CertificadoId, EmpresaId, RutaCertificado, Registrado, Status, ClaveArchivo
			var columns = [
				{field: "RutaCertificado", title: "RutaCertificado"},
				{field: "Registrado", title: "Registrado"},
				{field: "Status", title: "Status"},
				{field: "ClaveArchivo", title: "ClaveArchivo"},
				{field:"CertificadoId", title:"Acciones", width:"100px", 
				template: "<a  id='btn_ver' href='/Certificados/Item/#:CertificadoId#'><i _id='#:CertificadoId#' id='btn_editar' class='fa fa-pencil-square' title='Editar'></i></a>"}
			];

			function data_bound_items(e){
				$.each(e.sender.items(), function(index, item){
					item = $(item);
					//item.find("#btn_editar").on("click", edit_item);
				});
			}; 

			//INIT
			$.get('/Certificados/ObtenerCertificados/', function(response) {
				grid = create_grid(item_str, response, columns, data_bound_items);
			}, 'json');
		}

		function create_edit_window(){
			if(!$("#window_"+item_str).is(":visible")) return;
			var window, validator;

			function edit_item(event, data){
				console.log("entro a editar");
				var sender = $(event.currentTarget);
				var id = sender.attr("_id");
				form.find("[name='CertificadoId']").val(id);
				
				var item = sender.parent().parent();
				//var data = $("#grid_"+item_str).data("kendoGrid").dataItem(item);
				console.log("data edit" , data);

				response = data;
				set_data_object_to_form("form", data);
				//$("#window_"+item_str).data("kendoWindow").center().open();
			}

			function clear_controls(){
				form.find("input[name]").val("");
				form.find("[name='CertificadoId']").val("0");
				//$( "form input[name]" ).val("");
			}

			function register_item(){
				if(!validator.validate()) return false;

				var url = '/Certificados/GuardarCertificado';
				var data = get_data_object_from_form("form");
				var fn_compare_id = function(item, response){
						return item.servicioId == response.servicioId;
					}
				post_url(url, data, item_str, fn_compare_id);
				return false;
			}

			if(get_id_from_url() != 0)
				$.get('/Certificados/ObtenerCertificado/'+get_id_from_url(), function(response) {
					edit_item({ currentTarget: $("<span></span>").attr("_id", get_id_from_url()) }, response);
				}, 'json');
			//window = create_window_controls(item_str, clear_controls);
			validator = load_form_controls(item_str, register_item)
		}

		//INIT
		create_grid_items();
		create_edit_window();
	}

	//INIT MASTER
	load_items_certificados();			
});



