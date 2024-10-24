pipeline {
    agent any

    stages {
        stage('Checkout Code') {
            steps {
                echo 'Checking out code from GitHub...'
                // Lấy mã nguồn từ GitHub
                git branch: 'production', url: 'https://github.com/CaIIMeHieu/capygram-be.git'
            }
        }
        // stage('Analysising code with SonaQube') {
        //     environment {
        //         scannerHome = tool 'SonarQubeTool';
        //     }
        //     steps {
        //       withSonarQubeEnv(credentialsId: '3134872e-ceda-4659-92cc-22edbd81314d', installationName: 'SonarCloud') {
        //         sh "${scannerHome}/bin/sonar-scanner"
        //       }
        //     }
        // }
        stage('Run Docker Script') {
            steps {
                echo 'Running the Docker script...'
                sh 'chmod +x ./run_capygram_docker.sh'
                sh './run_capygram_docker.sh'
            }
        }
    }
}
