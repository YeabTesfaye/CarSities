﻿using Duende.IdentityServer.Models;

namespace IdentityService;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        [
           new("auctionApp", "Auction app full access"),
        ];

    public static IEnumerable<Client> Clients =>
        [
            new Client {
                ClientId = "postman",
                ClientName = "Postman",
                AllowedScopes = {"openid","profile","auctionApp"},
                RedirectUris = {"https://www.google.com"},
                ClientSecrets = [new Secret("NotASecret".Sha256())],
                AllowedGrantTypes = {GrantType.ResourceOwnerPassword}
            },
            new Client {
                ClientId = "nextApp",
                ClientName = "nextApp",
                AllowedScopes = {"openid","profile","auctionApp"},
                RedirectUris = {"http://localhost:3000/auth/callback/id-server"},
                ClientSecrets = [new Secret("secret".Sha256())],
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                RequirePkce = false,
                AllowOfflineAccess = true,
                AccessTokenLifetime = 3600*24*30
            }
        ];
}