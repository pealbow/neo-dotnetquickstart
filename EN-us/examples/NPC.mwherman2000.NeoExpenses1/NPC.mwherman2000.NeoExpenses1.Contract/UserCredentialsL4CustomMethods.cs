using Neo.SmartContract.Framework;
using NPC.Runtime;
using System;
using System.Numerics;

/// <summary>
/// NPC.mwherman2000.NeoExpenses1.Contract.UserCredentials Custom Methods
///
/// Processed:       2018-03-29 8:11:07 PM by npcc - NEO Persistable Classes (NPC) Platform 2.1 Compiler v2.1.0.34983
/// NPC Project:     https://github.com/mwherman2000/neo-npcc/blob/master/README.md
/// NPC Lead:        Michael Herman (Toronto) (mwherman@parallelspace.net)
/// </summary>

namespace NPC.mwherman2000.NeoExpenses1.Contract
{
    public partial class UserCredentials 
    {
        static readonly byte[] DOMAINUCD = "UCD".AsByteArray(); // User Credentials Directory (UCD)

        public static UserCredentials CreateUser(NeoVersionedAppUser AppVAU, object[] args)
        {
            UserCredentials results = UserCredentials.Null();

            byte[] encodedUsername = (byte[])args[0];
            if (NeoTrace.ARGSRESULTS) NeoTrace.Trace("AddUser.encodedUsername", encodedUsername);
            byte[] encodedPassword = (byte[])args[1];
            if (NeoTrace.ARGSRESULTS) NeoTrace.Trace("AddUser.encodedPassword", encodedPassword);

            UserCredentials uc = FindUser(AppVAU, encodedUsername);

            if (UserCredentials.IsMissing(uc)) // add the unique new user
            {
                if (NeoTrace.INFO) UserCredentials.LogExt("AddUser.missing", uc);
                uc = UserCredentials.New(encodedUsername, encodedPassword);
                UserCredentials.PutElement(uc, AppVAU, DOMAINUCD, encodedUsername);
                if (NeoTrace.INFO) UserCredentials.LogExt("AddUser.added", uc);
            }
            else
            {
                if (NeoTrace.INFO) UserCredentials.LogExt("AddUser.exists", uc);
            }

            results = uc;
            if (NeoTrace.ARGSRESULTS) NeoTrace.Trace("AddUser.results", results);

            return results;
        }

        private static UserCredentials FindUser(NeoVersionedAppUser AppVAU, byte[] encodedUsername)
        {
            if (NeoTrace.ARGSRESULTS) NeoTrace.Trace("FindUser.encodedUsername", encodedUsername);

            UserCredentials result = UserCredentials.GetElement(AppVAU, DOMAINUCD, encodedUsername);

            if (NeoTrace.ARGSRESULTS) UserCredentials.LogExt("FindUser.result", result);

            return result;
        }

        public static UserCredentials GetUser(NeoVersionedAppUser AppVAU, object[] args)
        {
            UserCredentials results = UserCredentials.Null();

            byte[] encodedUsername = (byte[])args[0];
            if (NeoTrace.ARGSRESULTS) NeoTrace.Trace("GetUser.encodedUsername", encodedUsername);

            UserCredentials uc = FindUser(AppVAU, encodedUsername);
            if (NeoTrace.INFO) UserCredentials.LogExt("GetUser.uc", uc);

            results = uc;
            if (NeoTrace.ARGSRESULTS) NeoTrace.Trace("GetUser.results", results);

            return results;
        }

        //private static UserCredentials[] GetUsers(NeoVersionedAppUser AppVAU, object[] args)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
