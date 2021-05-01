<?php
	
	//Tomo del form que viene de Unity los strings que guarde con los respectivos nombres
	$servername = $_POST["host"];
	$database = $_POST["database"];
	$server_username = $_POST["dbuser"];
	$server_password = $_POST["dbpw"];

	//mysqli_connect() intenta abrir una conexión al servidor MySQL
	//En caso exitoso, regresará un objeto representando la conexión a la base de datos, o FALSE en caso contrario
	$con = mysqli_connect($servername, $server_username, $server_password, $database);

	//Chequeo si no hubo problema en la conexion
	if (mysqli_connect_errno())
	{
		echo "1: Connection failed"; //Si hubo, devuelvo un string
		exit(); //y salgo de la ejecucion
	}

	//Tomo los demas datos del form creado en Unity
	$username = $_POST["user"];
	$pw = $_POST["pass"];

	//Guardo la sentencia en una variable para usarla en el query
	$insertuserquery = "INSERT INTO accounts (Username, Password) VALUES ('" . $username . "',
																		'" . $pw . "');";

	//Envio la sentencia a la conexion establecida (por eso utilizo la variable donde hice mi conexion $con)
	//Si hubo error "salgo" con or die y un string
	mysqli_query($con, $insertuserquery) or die("2: Insert player query failed"); //or die($con->error); //Error code #2 = Query failed

	//Si llego hasta aca quiere decir que no hubo ningun problema y devuelvo un 0 para luego utilizarlo como condicion en Unity
	echo ("0");

	//Cierro la conexion
	mysqli_close($con);

?>