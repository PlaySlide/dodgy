import HutongGames.PlayMaker;

//var globalVariables = FsmVariables.GlobalVariables;
//var shootZ = globalVariables.GetFsmFloat ("shootZ");
//var incomingPowerX = globalVariables.GetFsmFloat ("shootX");
//var incomingPowerY = globalVariables.GetFsmFloat ("shootY");

//var incomingAngle = globalAngle;
//var incomingAngle = shootZ;
var outgoingPowerX = 0.0;
var outgoingPowerY = 90.0;

function getShootAngle(incomingAngle : float){
	if (incomingAngle < 0){
		incomingAngle = incomingAngle + 360;
		} 

	if (incomingAngle == 0) {
		outgoingPowerX = -90.0;
		outgoingPowerY = 0.0;
	}

	if (incomingAngle >= 1.0 && incomingAngle <= 89.0) {
		outgoingPowerX = -90.0;
		outgoingPowerY = 0.0;
		for (var i = 0.0; i < incomingAngle; i++){
			outgoingPowerX++; 
			outgoingPowerY--;
		}
	}
			// 90
	if (incomingAngle == 90.0) {
		outgoingPowerX = 90.0;
		outgoingPowerY = 0.0;
	}

	if (incomingAngle >= 91.0 && incomingAngle <= 179.0) {
		outgoingPowerX = 90.0;
		outgoingPowerY = 0.0;
		for (var j = 0.0; j < (180 - incomingAngle); j++){
			outgoingPowerX--; 
			outgoingPowerY--;
		}
	}

	// 180	
	if (incomingAngle == 180.0) {
		outgoingPowerX = 90.0;
		outgoingPowerY = 0.0;
	}
	if (incomingAngle >= 181.0 && incomingAngle <= 269.0) {
		outgoingPowerX = 90.0;
		outgoingPowerY = 0.0;
		for (var k = 0.0; k < (270.0 - incomingAngle); k++){
			outgoingPowerX--; 
			outgoingPowerY++;
		}
	} 	

	//270
	if (incomingAngle == 270.0) {
		outgoingPowerX = -90.0;
		outgoingPowerY = 0.0;	
		}

	if (incomingAngle >= 271.0 && incomingAngle <= 359.0) {
		outgoingPowerX = -90.0;
		outgoingPowerY = 0.0;
		for (var m = 0; m < (360.0 - incomingAngle); m++){
			outgoingPowerX++; 
			outgoingPowerY++;
		}
	}
}