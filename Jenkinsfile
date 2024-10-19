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
            steps {
                script {
                    def sqScannerMsBuildHome = tool 'SonarScanner'
                    withSonarQubeEnv('SonarCloud') {
                    bat "${sqScannerMsBuildHome}\\SonarQube.Scanner.MSBuild.exe begin /k:myKey"
                    bat 'MSBuild.exe /t:Rebuild'
                    bat "${sqScannerMsBuildHome}\\SonarQube.Scanner.MSBuild.exe end"
                }
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
