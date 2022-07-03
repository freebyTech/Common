@Library('freebyTech')_

import com.freebyTech.BuildInfo
import com.freebyTech.NugetPushOptionEnum

String versionPrefix = '1.3'
String repository = 'freebytech'    
String imageName = 'common'
String dockerBuildArguments = ''

node 
{
  build(this, versionPrefix, repository, imageName, dockerBuildArguments, false, false, NugetPushOptionEnum.PushDebug, 'freebyTech.Common')
}

