using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class UpgradeShop : Shop
{




    //Qd passar o gun, eu j� passo o dicionario com o valor
    public void BuyUpgrade (UpgradeInfo referenceUpgrade, Dictionary<UpgradeInfo, (Action<UpgradeInfo>, UpgradeInfo)> referenceToInstantiadedDictionary)
    {

        UpgradeInfo instantiatedUpgrade = referenceToInstantiadedDictionary[referenceUpgrade].Item2;
        Action <UpgradeInfo> funtionToCurrentUpgrade = referenceToInstantiadedDictionary[referenceUpgrade].Item1;



        if (instantiatedUpgrade.useLimit > 0)
        {
            if (CanPlayerBuy(instantiatedUpgrade.price))
            {
                SpendPlayerPointsOnBuy(instantiatedUpgrade.price);

                funtionToCurrentUpgrade(instantiatedUpgrade); //VER SE ISSO TA RODANDO

                instantiatedUpgrade.useLimit -= 1; //Como vai mudar o valor, mudar o valor do upgrade intanciado e n�o o original
                instantiatedUpgrade.price *= 2; //O pre�o duplica ap�s a compra
            }
            else
            {
                //Meter um pop-up dizendo que o jogador est� ruim das contas? >_<
            }
        }





    }

    


    






}
