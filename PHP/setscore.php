<?php
	
	//Tomo del form que viene de Unity los strings que guardE con los respectivos nombres
	$servename = $_POST["host"];
	$database = $_POST["database"];
	$srvr_user = $_POST["dbuser"];
	$srvr_pw = $_POST["dbpw"];

	//mysqli_connect() intenta abrir una conexion al servidor MySQL
	//En caso de tener exito, regresa un objeto representando la conexion a la base de datos. De no tener exito regresa FALSE
	$con = mysqli_connect($servename, $srvr_user, $srvr_pw, $database);

	//Chequeo si no hubo problema con la conexion
	if (mysqli_connect_errno())
	{
		echo("1: Connection failed"); //Si hubo, devuelvo un string
		exit(); //y salgo de la ejecucion
	}

	//Tomo los demas datos del form creado en Unity
	$username = $_POST["user"];
	$score = $_POST["score"];

	/////$queryCheck = $_POST["queryCheck"];

	//Armo una sentencia para ver si existe ese usuario
	$queryCheck = "SELECT user FROM `highscores` WHERE user = ('".$username."');";

	//Ejecuto la sentencia y recibo el resultado en una variable
	$result = mysqli_query($con, $queryCheck); 

	//Creo la variable donde voy a ejecutar una futura sentencia
	$insertscorequery = "";

	//Si el resultado de la query anterior no me devolvio ninguna linea (o sea que no existe el usuario en la tabla highscores)
	if (mysqli_num_rows($result) != 1)
	{
		/////$insertscorequery = $_POST["queryNoUser"];

		//Agrego ese usuario con su score a la tabla
		$insertscorequery = "INSERT INTO `highscores` (user, score) VALUES ('" . $username . "', '" . $score . "');";
	}
	else
	{
		/////$insertscorequery = $_POST["queryUser"];

		//Sino (si existe el usuario porque ya tuvo un score), se lo actualizo con el nuevo
		$insertscorequery = "UPDATE `highscores` SET score = (" . $score . ") WHERE user = ('" . $username . "');";
	}

	//Ejecuto la sentencia del if - else
	mysqli_query($con, $insertscorequery) or die ($con->error);

	echo("0");
	
	//Cierro la conexion
	mysqli_close($con);

?>