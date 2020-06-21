﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OventService
{
    internal class OventApiService
    {
        private HttpClient Client;
        public OventApiService(string host)
        {
            InitializeClient(host);
        }

        private void InitializeClient(string host)
        {
            this.Client = new HttpClient();
            this.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "YWRtaW46N0RUdXk2VEs=");
            this.Client.BaseAddress = new Uri(host);
        }

        private string ToApiRoute(string device,string commande) => $"ZAutomation/api/v1/devices/{device}/command/{commande}";

        private async Task GetAsync(string device, string commande = "on") => await this.Client.GetAsync(ToApiRoute(device, commande));

        public async Task AllumerLaLumiereDuSalonAsync() => await GetAsync("Scenes_5");

        public async Task EteindreLaLumiereDuSalonAsync() => await GetAsync("Scenes_10");

        public async Task AllumerLaLumiereDuPCAsync() => await GetAsync("ZWayVDev_zway_11-0-37");

        public async Task EteindreLaLumiereDuPCAsync() => await GetAsync("ZWayVDev_zway_11-0-37","off");

        public async Task AllumerLaLumiereTVAsync() => await GetAsync("ZWayVDev_zway_2-0-37");

        public async Task EteindreLaLumiereTVAsync() => await GetAsync("ZWayVDev_zway_2-0-37","off");

        public async Task OuvrirLesVoletsAsync() => await GetAsync("Scenes_6");

        public async Task FermerLesVoletsAsync() => await GetAsync("Scenes_11");

        public async Task OuvrirVoletPCAsync() => await GetAsync("ZWayVDev_zway_9-0-37");

        public async Task FermerVoletPCAsync() => await GetAsync("ZWayVDev_zway_9-0-37","off");

        public async Task OuvrirVoletTVAsync() => await GetAsync("ZWayVDev_zway_8-0-37");

        public async Task FermerVoletsTVAsync() => await GetAsync("ZWayVDev_zway_8-0-37", "off");

        public async Task FermeToutAsync() => await GetAsync("Scenes_8");

        public async Task AllumeEteinsLaMusiqueAsync() => await GetAsync("BoseSoundTouchCommand_31");

        public async Task PlayPauseLaMusiqueAsync() => await GetAsync("BoseSoundTouchCommand_32");

        public async Task SelectionneLePreset1Async() => await GetAsync("BoseSoundTouchCommand_34");

        public async Task SelectionneLePreset2Async() => await GetAsync("BoseSoundTouchCommand_35");

        public async Task SelectionneLePreset3Async() => await GetAsync("BoseSoundTouchCommand_36");

        public async Task SelectionneLePreset4Async() => await GetAsync("BoseSoundTouchCommand_37");

        public async Task SelectionneLePreset5Async() => await GetAsync("BoseSoundTouchCommand_38");

        public async Task SelectionneLePreset6Async() => await GetAsync("BoseSoundTouchCommand_39");

    }
}
