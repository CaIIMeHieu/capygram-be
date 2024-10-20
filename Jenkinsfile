pipeline {
    agent any

    stages {
        stage('Checkout Code') {
            steps {
                echo 'Checking out code from GitHub...'
                // Lấy mã nguồn từ GitHub
                git branch: 'master', url: 'https://github.com/CaIIMeHieu/capygram-be.git'
            }
        }
        stage('Analysising code with SonaQube') {
            // steps {
            //     script {
            //         def sqScannerMsBuildHome = tool 'SonarScanner'
                    
            //         withSonarQubeEnv('SonarQube') {
            //         sh "${sqScannerMsBuildHome}/sonar-scanner-5.0.1.3006/bin/sonar-scanner \
            //             -Dsonar.projectKey=CaIIMeHieu_capygram-be \
            //             -Dsonar.sources=. \
            //             -Dsonar.host.url=https://sonarcloud.io \
            //             -Dsonar.login=ac3980b0126c98b08cf76c6f134be18f433c3652"
            //         }
            //     }
            // }
            environment {
                scannerHome = tool 'SonarQubeTool';
            }
            steps {
              withSonarQubeEnv(credentialsId: 'Secret text', installationName: 'SonarCloud') {
                sh "${scannerHome}/bin/sonar-scanner"
              }
            }
        }
        stage('Run Docker Script') {
            steps {
                echo 'Running the Docker script...'
                // Cấp quyền thực thi cho script (nếu cần)
                sh 'chmod +x ./run_capygram_docker.sh'
                // Thực thi script quản lý Docker containers
                sh './run_capygram_docker.sh'
            }
        }
    }

    post {
        always {
            echo 'Cleaning up workspace...'
            cleanWs()
        }
    }
}
